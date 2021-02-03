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

        /// <summary>
        /// 
        /// </summary>
        /// <param name="logger"></param>
        /// <param name="next"></param>
        public GrafanaControllersMiddleware(IApiLogger logger, RequestDelegate next, IMetricFactory metricFactory)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _next = next ?? throw new ArgumentNullException(nameof(next));
            _metricFactory = metricFactory ?? throw new ArgumentNullException(nameof(next));
            _logger.LogDebug("Exception handling middleware initialized");
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public async Task Invoke(HttpContext context)
        {
            using var metric = _metricFactory.CreateWhitebox(new Operation(MetricCategory.Http, context.Request.Path, context.Request.Method, "1.0"));
            try
            {
                await _next(context);
            }
            catch (Exception e)
            {
                metric.Result = MetricResult.Failure;
                throw;
            }
        }

    }
}
