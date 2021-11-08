using Aether.QualityChecks.Interfaces;
using Aether.QualityChecks.Models;
using APILogger.Interfaces;
using Ardalis.GuardClauses;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace Aether.QualityChecks.Middleware
{
    public class QualityCheckMiddleware
    {
        private readonly string _qualityTestRoute;
        
        private readonly IEnumerable<IQualityCheck> _tests;
        private readonly Type _typeFilter;
        private readonly IApiLogger _logger;
        private readonly RequestDelegate _next;

        public QualityCheckMiddleware(IApiLogger logger, RequestDelegate next, IEnumerable<IQualityCheck> tests, string qualityTestRoute)
        {
            _tests = Guard.Against.Null(tests, nameof(tests));
            _logger = Guard.Against.Null(logger, nameof(logger));
            _next = next;

            Guard.Against.NullOrWhiteSpace(qualityTestRoute, nameof(qualityTestRoute));
            _qualityTestRoute = Guard.Against.InvalidInput(qualityTestRoute, nameof(qualityTestRoute), s => s.ElementAt(0).Equals('/'));

            _logger.LogDebug($"{nameof(QualityCheckMiddleware)} initialized with {qualityTestRoute}");
        }

        public QualityCheckMiddleware(IApiLogger logger, RequestDelegate next, IEnumerable<IQualityCheck> tests, string qualityTestRoute, Type typeFilter)
        {
            _tests =        Guard.Against.Null(tests, nameof(tests));
            _typeFilter =   Guard.Against.Null(typeFilter, nameof(typeFilter));
            _logger = Guard.Against.Null(logger, nameof(logger));
            _next = next;

            Guard.Against.NullOrWhiteSpace(qualityTestRoute, nameof(qualityTestRoute));
            _qualityTestRoute = Guard.Against.InvalidInput(qualityTestRoute, nameof(qualityTestRoute), s => s.ElementAt(0).Equals('/'));

            _logger.LogDebug($"{nameof(QualityCheckMiddleware)} initialized with {qualityTestRoute}");
        }

        public async Task Invoke(HttpContext context)
        {
            Guard.Against.Null(context?.Request?.Path.Value, nameof(context));

            if (context.Request.Path.Value.ToLower().Contains(_qualityTestRoute.ToLower()))
            {
                _logger.LogDebug($"{nameof(QualityCheckMiddleware)} Running {_tests.Count()} tests");

                var testResults = new List<QualityCheckResponseModel>();

                var filteredTests = ApplyTypeFilter(_tests, _typeFilter);

                foreach (var test in filteredTests)
                {
                    QualityCheckResponseModel response = new QualityCheckResponseModel(null);

                    try
                    {
                        _logger.LogInfo($"running {test.LogName}");
                        response = await test.RunAsync();
                    }
                    catch (Exception ex)
                    {
                        _logger.LogError(test.LogName, exception: ex);
                    }
                    finally
                    {
                        await test.TearDownAsync();

                        string status = response.CheckPassed ? "Passed" : "Failed";

                        _logger.LogInfo($"{test.LogName} status: {status}");

                        testResults.Add(response);
                    }
                }

                _logger.LogDebug($"{nameof(QualityCheckMiddleware)} Test Results", testResults);

                context.Response.StatusCode = testResults.All(t => t.CheckPassed) ? (int) HttpStatusCode.OK
                                                                                  : (int) HttpStatusCode.InternalServerError;

                context.Response.ContentType = "application/json";
                await context.Response.WriteAsync(JsonConvert.SerializeObject(testResults));
            }
            else
            {
                await _next(context);
            }
        }

        private static IEnumerable<IQualityCheck> ApplyTypeFilter(IEnumerable<IQualityCheck> tests, Type typeFilter)
        {
            foreach (var test in tests)
            {
                if (typeFilter != null)
                {
                    var interfaces = test.GetType().GetInterfaces();

                    if (interfaces.Any(i => i.GenericTypeArguments.Contains(typeFilter)))
                    {
                        yield return test;
                    }
                }
                else
                {
                    yield return test;
                }
            }
        }
    }
}
