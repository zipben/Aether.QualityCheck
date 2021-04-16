using Aether.Interfaces;
using APILogger.Interfaces;
using Ardalis.GuardClauses;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Aether.Middleware
{
    public class QualityCheckMiddleware
    {
        private const string QUALITY_TEST_ROUTE = "/api/QualityCheck";

        private readonly RequestDelegate _next;
        private readonly IApiLogger _logger;
        private readonly IEnumerable<IQualityCheck> _tests;

        public QualityCheckMiddleware(IApiLogger logger, IEnumerable<IQualityCheck> tests, RequestDelegate next)
        {
            Guard.Against.Null(logger, nameof(logger));
            Guard.Against.Null(next, nameof(next));
            Guard.Against.Null(tests, nameof(tests));

            _logger = logger;
            _next = next;
            _tests = tests;

            _logger.LogDebug("Quality Check middleware initialized");
        }
 
        public async Task Invoke(HttpContext context)
        {
            _logger.LogDebug("QualityCheckRouteLog:" + context.Request.Path.Value);

            if (context.Request.Path.Value.Contains(QUALITY_TEST_ROUTE))
            {
                _logger.LogDebug($"{nameof(QualityCheckMiddleware)} Running {_tests.Count()} tests");

                List<bool> testResults = new List<bool>();

                foreach (var test in _tests)
                    testResults.Add(await test.Run());

                _logger.LogDebug($"{nameof(QualityCheckMiddleware)} " +
                    $"Tests Passed: {testResults.Where(t => t == true).Count()} " +
                    $"Tests Failed: {testResults.Where(t => t == false).Count()}");

                if (testResults.Any(t => t == false))
                    context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                else
                    context.Response.StatusCode = (int)HttpStatusCode.OK;
            }
            else
            {
                await _next(context);
            }
        }

    }
}
