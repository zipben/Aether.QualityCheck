﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using APILogger.Interfaces;
using Microsoft.AspNetCore.Http;
using RockLib.Metrics;

namespace Aether.Middleware
{
    public class GrafanaControllersMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly IMetricFactory _metricFactory;
        private readonly IApiLogger _logger;
        private static readonly List<string> _filterList = new List<string> { "/api/heartbeat" };

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

        private bool IsInFilter(HttpContext context) =>
            _filterList.Contains(context.Request.Path);
    }
}
