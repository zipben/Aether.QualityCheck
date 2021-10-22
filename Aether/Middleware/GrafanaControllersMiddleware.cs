﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Aether.Attributes;
using APILogger.Interfaces;
using Ardalis.GuardClauses;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Features;
using RockLib.Metrics;
using Microsoft.AspNetCore.Http;
using Aether.Extensions;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System.Reflection;

namespace Aether.Middleware
{
    public class GrafanaControllersMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly IMetricFactory _metricFactory;
        private readonly IApiLogger _logger;
        private readonly List<string> _filterList = new List<string>();

        /// <summary>
        /// Constructor for GrafanaControllersMiddleware
        /// </summary>
        /// <param name="logger"></param>
        /// <param name="next"></param>
        public GrafanaControllersMiddleware(IApiLogger logger, IMetricFactory metricFactory, RequestDelegate next, List<string> filterList)
        {
            _logger =           Guard.Against.Null(logger, nameof(logger));
            _next =             Guard.Against.Null(next, nameof(next));
            _metricFactory =    Guard.Against.Null(metricFactory, nameof(metricFactory));
            _filterList =       Guard.Against.Null(filterList, nameof(filterList));

            _logger.LogDebug("GrafanaControllersMiddleware initialized");
        }

        /// <summary>
        /// Invoke method for GrafanaControllersMiddleware. Adds Grafana metrics to all HTTP methods.
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public async Task Invoke(HttpContext context)
        {
            if (IsInFilter(context))
            {
                try
                {
                    await _next(context);
                }
                catch (Exception)
                {
                    throw;
                }
            }
            else
            {
            

                Operation operation;
                if (context.Request.QueryString.HasValue)
                {
                    var metricName = $"{context.Request.Path}{context.Request.QueryString.Value}";
                    operation = new Operation(MetricCategory.Http, metricName, context.Request.Method, "1.0");
                }
                else
                {
                    operation = new Operation(MetricCategory.Http, context.Request.Path, context.Request.Method, "1.0");
                }

                using var metric = _metricFactory.CreateWhitebox(operation);

                try
                {
                    var body = await context.Request.PeekBodyAsync();

                    await _next(context);

                    TryCaptureCustomMetrics(context, body);

                }
                catch (Exception)
                {
                    metric.Result = MetricResult.Failure;
                    throw;
                }
            }
        }

        private void TryCaptureCustomMetrics(HttpContext context, string body)
        {
            try
            {
                var endpoint = context.GetEndpoint();

                var paramAttribute = endpoint?.Metadata.GetMetadata<ParamMetricAttribute>();
                var bodyAttribute = endpoint?.Metadata.GetMetadata<BodyMetricAttribute>();

                if (paramAttribute != null)
                {
                    CaptureCustomParamMetric(context, paramAttribute, endpoint.DisplayName);
                }
                else if (bodyAttribute != null && body.Exists())
                {
                    CaptureCustomBodyMetric(context, bodyAttribute, body, endpoint.DisplayName);
                }
            }
            catch(Exception e)
            {
                _logger.LogError(nameof(TryCaptureCustomMetrics), exception: e);
            }
        }

        private void CaptureCustomBodyMetric(HttpContext context, BodyMetricAttribute bodyAttribute, string body, string metricName)
        {
            var bodyObj = JsonConvert.DeserializeObject(body, bodyAttribute.BodyType);

            Dictionary<string, string> paramValues = new Dictionary<string, string>();

            PropertyInfo[] props = bodyAttribute.BodyType.GetProperties();

            props = props.Where(p => bodyAttribute.Params.Contains(p.Name)).ToArray();

            foreach(var prop in props)
            {
                paramValues[prop.Name] = prop.GetValue(bodyObj).ToString();
            }

            CaptureCustomMetric(paramValues, metricName);
        }

        private void CaptureCustomParamMetric(HttpContext context, ParamMetricAttribute paramAttribute, string metricName)
        {
            Dictionary<string, string> paramValues = new Dictionary<string, string>();

            foreach(var param in paramAttribute.Params)
            {
                context.Request.Query.TryGetValue(param, out var qValue);
                context.Request.RouteValues.TryGetValue(param, out var rValue);

                if (qValue.ToString().Exists())
                    paramValues[param] = qValue.ToString();
                else if(rValue != null && rValue.ToString().Exists())
                    paramValues[param] = rValue.ToString();
            }

            CaptureCustomMetric(paramValues, metricName);
        }

        private void CaptureCustomMetric(Dictionary<string, string> fields, string metricName)
        {
            foreach(var field in fields)
            {
                _metricFactory.CreateWhitebox(new Operation(MetricCategory.Other, $"{metricName}/{field.Key}={field.Value}"));
            }
        }


        private bool IsInFilter(HttpContext context)
        {
            foreach(var filter in _filterList)
            {
                Regex rgx = new Regex(filter);

                if (rgx.IsMatch(context.Request.Path))
                    return true;
            }

            return false;
        }
    }
}
