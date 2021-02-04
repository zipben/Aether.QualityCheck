using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using APILogger.Interfaces;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using RockLib.Logging;
using RockLib.Metrics;

namespace Aether.Middleware
{
    public class GrafanaControllersMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly IMetricFactory _metricFactory;
        private readonly IApiLogger _logger;
        private static readonly List<string> _filterList = new List<string>{
            "/api/heartbeat"
        };

        /// <summary>
        /// Constructor for GrafanaControllersMiddleware
        /// </summary>
        /// <param name="logger"></param>
        /// <param name="next"></param>
        public GrafanaControllersMiddleware(IApiLogger logger, RequestDelegate next, IMetricFactory metricFactory)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _next = next ?? throw new ArgumentNullException(nameof(next));
            _metricFactory = metricFactory ?? throw new ArgumentNullException(nameof(metricFactory));
            _logger.LogDebug("Exception handling middleware initialized");
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
                using var metric = _metricFactory.CreateWhitebox(new Operation(MetricCategory.Http, context.Request.Path, context.Request.Method, "1.0"));
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

        private bool IsInFilter(HttpContext context) => _filterList.Contains(context.Request.Path);
}
