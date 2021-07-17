using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using APILogger.Interfaces;
using Ardalis.GuardClauses;
using Microsoft.AspNetCore.Http;
using RockLib.Metrics;

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
        public GrafanaControllersMiddleware(IApiLogger logger, IMetricFactory metricFactory, RequestDelegate next, List<string> filterList = null)
        {
            _logger =           Guard.Against.Null(logger, nameof(logger));
            _next =             Guard.Against.Null(next, nameof(next));
            _metricFactory =    Guard.Against.Null(metricFactory, nameof(metricFactory));

            if (filterList != null)
                _filterList = filterList.ToList();

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
                    await _next(context);
                }
                catch (Exception)
                {
                    metric.Result = MetricResult.Failure;
                    throw;
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
