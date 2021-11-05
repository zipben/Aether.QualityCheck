using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using APILogger.Interfaces;
using Ardalis.GuardClauses;
using Microsoft.AspNetCore.Http;
using RockLib.Metrics;

namespace Aether.Middleware
{
    public class GrafanaControllersMiddleware : MiddlewareBase
    {
        private readonly IMetricFactory _metricFactory;
        private readonly List<string> _filterList = new List<string>();

        /// <summary>
        /// Constructor for GrafanaControllersMiddleware
        /// </summary>
        /// <param name="logger"></param>
        /// <param name="next"></param>
        public GrafanaControllersMiddleware(IApiLogger logger, RequestDelegate next, IMetricFactory metricFactory, List<string> filterList) : base(logger, next)
        {
            _metricFactory =    Guard.Against.Null(metricFactory, nameof(metricFactory));
            _filterList =       Guard.Against.Null(filterList, nameof(filterList));

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
                    await _next(context);
                }
                catch (Exception)
                {
                    metric.Result = MetricResult.Failure;
                    throw;
                }
            }
        }
    }
}
