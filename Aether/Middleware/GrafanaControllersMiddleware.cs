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
using System.Threading.Tasks;

namespace Aether.Middleware
{
    public class GrafanaControllersMiddleware : MiddlewareBase
    {
        private readonly IMetricFactory _metricFactory;
        private readonly List<string> _filterList = new List<string>();
        private readonly IHttpContextUtils _httpContextUtils;

        /// <summary>
        /// Constructor for GrafanaControllersMiddleware
        /// </summary>
        /// <param name="logger"></param>
        /// <param name="next"></param>
        public GrafanaControllersMiddleware(IApiLogger logger, RequestDelegate next, IMetricFactory metricFactory, List<string> filterList, IHttpContextUtils httpContextUtils) : base(logger, next)
        {
            _metricFactory =    Guard.Against.Null(metricFactory, nameof(metricFactory));
            _filterList =       Guard.Against.Null(filterList, nameof(filterList));
            _httpContextUtils = Guard.Against.Null(httpContextUtils, nameof(httpContextUtils));

            _logger.LogDebug($"{nameof(GrafanaControllersMiddleware)} initialized");
        }

        /// <summary>
        /// Invoke method for GrafanaControllersMiddleware. Adds Grafana metrics to all HTTP methods.
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public async Task Invoke(HttpContext context)
        {
            if (IsInFilter(context, _filterList))
            {
                await _next(context);
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

                string body;

                using (var metric = _metricFactory.CreateWhitebox(operation))
                {
                    try
                    {
                        //must happen before the delegate fires because the delegate consumes the body
                        body = await _httpContextUtils.PeekRequestBodyAsync(context);

                        await _next(context);
                    }
                    catch (Exception)
                    {
                        metric.Result = MetricResult.Failure;
                        throw;
                    }
                }

                TryCaptureCustomMetrics(context, body);                
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
                    CaptureCustomBodyMetric(bodyAttribute, body);
                }
            }
            catch (Exception e)
            {
                _logger.LogError(nameof(TryCaptureCustomMetrics), exception: e);
            }
        }

        private void CaptureCustomBodyMetric(BodyMetricAttribute bodyAttribute, string body)
        {
            var bodyObj = JsonConvert.DeserializeObject(body, bodyAttribute.BodyType);

            Dictionary<string, string> paramValues = new Dictionary<string, string>();

            PropertyInfo[] props = bodyAttribute.BodyType.GetProperties();

            props = props.Where(p => bodyAttribute.Params.Contains(p.Name)).ToArray();

            foreach (var prop in props)
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
                {
                    paramValues[param] = qValue.ToString();
                }
            }

            CaptureCustomMetric(paramValues, paramAttribute.MetricName);
        }

        private void CaptureCustomMetric(Dictionary<string, string> tags, string metricName)
        {
            using var client = _metricFactory.Client;
            client.Write(metricName, new Dictionary<string, object>(), tags);
        }
    }
}
