using Aether.Attributes;
using Aether.Extensions;
using Aether.Helpers;
using APILogger.Interfaces;
using Ardalis.GuardClauses;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Features;
using Newtonsoft.Json;
using RockLib.Metrics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;
using System.Threading.Tasks;


namespace Aether.Middleware
{
    public class GrafanaControllersMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly IMetricFactory _metricFactory;
        private readonly IApiLogger _logger;
        private readonly IHttpContextUtils _httpContextUtils;
        private readonly List<string> _filterList = new List<string>();

        /// <summary>
        /// Constructor for GrafanaControllersMiddleware
        /// </summary>
        /// <param name="logger"></param>
        /// <param name="next"></param>
        public GrafanaControllersMiddleware(IApiLogger logger, IMetricFactory metricFactory, RequestDelegate next, List<string> filterList, IHttpContextUtils httpContextUtils)
        {
            _logger =           Guard.Against.Null(logger, nameof(logger));
            _next =             Guard.Against.Null(next, nameof(next));
            _metricFactory =    Guard.Against.Null(metricFactory, nameof(metricFactory));
            _filterList =       Guard.Against.Null(filterList, nameof(filterList));
            _httpContextUtils = Guard.Against.Null(httpContextUtils, nameof(httpContextUtils));

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
                    var body = await _httpContextUtils.PeekRequestBodyAsync(context);

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
                var endpoint = context.Features.Get<IEndpointFeature>()?.Endpoint;

                var paramAttribute = endpoint?.Metadata.GetMetadata<ParamMetricAttribute>();
                var bodyAttribute = endpoint?.Metadata.GetMetadata<BodyMetricAttribute>();

                if (paramAttribute != null)
                {
                    CaptureCustomParamMetric(context, paramAttribute);
                }
                else if (bodyAttribute != null && body.Exists())
                {
                    CaptureCustomBodyMetric(context, bodyAttribute, body);
                }
            }
            catch(Exception e)
            {
                _logger.LogError(nameof(TryCaptureCustomMetrics), exception: e);
            }
        }

        private void CaptureCustomBodyMetric(HttpContext context, BodyMetricAttribute bodyAttribute, string body)
        {
            var bodyObj = JsonConvert.DeserializeObject(body, bodyAttribute.BodyType);

            Dictionary<string, string> paramValues = new Dictionary<string, string>();

            PropertyInfo[] props = bodyAttribute.BodyType.GetProperties();

            props = props.Where(p => bodyAttribute.Params.Contains(p.Name)).ToArray();

            foreach(var prop in props)
            {
                paramValues[prop.Name] = prop.GetValue(bodyObj).ToString();
            }

            CaptureCustomMetric(paramValues, bodyAttribute.MetricName);
        }

        private void CaptureCustomParamMetric(HttpContext context, ParamMetricAttribute paramAttribute)
        {
            Dictionary<string, string> paramValues = new Dictionary<string, string>();

            foreach(var param in paramAttribute.Params)
            {
                context.Request.Query.TryGetValue(param, out var qValue);

                if (qValue.ToString().Exists())
                    paramValues[param] = qValue.ToString();
            }

            CaptureCustomMetric(paramValues, paramAttribute.MetricName);
        }

        private void CaptureCustomMetric(Dictionary<string, string> fields, string metricName)
        {
            using (var client = _metricFactory.Client)
            {
                foreach(var field in fields)
                {
                    client.Write($"{metricName}_{field.Key}", field.Value);
                }
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
