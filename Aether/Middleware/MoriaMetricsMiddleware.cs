using System.Collections.Generic;
using System.Threading.Tasks;
using Aether.Helpers.Interfaces;
using APILogger.Interfaces;
using Ardalis.GuardClauses;
using Microsoft.AspNetCore.Http;

namespace Aether.Middleware
{
    public class MoriaMetricsMiddleware : MiddlewareBase
    {
        private readonly IMoriaPublisher _moriaPublisher;
        private readonly List<string> _filterList = new List<string>();

        /// <summary>
        /// Constructor for GrafanaControllersMiddleware
        /// </summary>
        /// <param name="logger"></param>
        /// <param name="next"></param>
        public MoriaMetricsMiddleware(IApiLogger logger, RequestDelegate next, IMoriaPublisher moriaPublisher, List<string> filterList) : base(logger, next)
        {
            _moriaPublisher =   Guard.Against.Null(moriaPublisher, nameof(moriaPublisher));
            _filterList =       Guard.Against.Null(filterList, nameof(filterList));

            _logger.LogDebug($"{nameof(MoriaMetricsMiddleware)} initialized");
        }

        /// <summary>
        /// Invoke method for GrafanaControllersMiddleware. Adds Grafana metrics to all HTTP methods.
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public async Task Invoke(HttpContext context)
        {
            if (!IsInFilter(context, _filterList))
            {
                await _moriaPublisher.CaptureMetricEvent(context.Request);
            }
            await _next(context);
        }
    }
}
