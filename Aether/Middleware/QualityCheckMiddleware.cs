using Aether.Interfaces;
using Aether.Models;
using APILogger.Interfaces;
using Ardalis.GuardClauses;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace Aether.Middleware
{
    public class QualityCheckMiddleware
    {
        private readonly string _qualityTestRoute;
        
        private readonly RequestDelegate _next;
        private readonly IApiLogger _logger;
        private readonly Type _typeFilter;

        private IEnumerable<IQualityCheck> _tests;

        public QualityCheckMiddleware(IApiLogger logger, IEnumerable<IQualityCheck> tests, RequestDelegate next, string qualityTestRoute)
        {
            _logger =   Guard.Against.Null(logger, nameof(logger));
            _next =     Guard.Against.Null(next, nameof(next));
            _tests =    Guard.Against.Null(tests, nameof(tests));

            Guard.Against.NullOrWhiteSpace(qualityTestRoute, nameof(qualityTestRoute));
            _qualityTestRoute = Guard.Against.InvalidInput(qualityTestRoute, nameof(qualityTestRoute), delegate (string s) { return s.ElementAt(0).Equals('/'); });

            _logger.LogDebug($"Quality Check middleware initialized with {qualityTestRoute}");
        }

        public QualityCheckMiddleware(IApiLogger logger, IEnumerable<IQualityCheck> tests, RequestDelegate next, string qualityTestRoute, Type typeFilter)
        {
            _logger =       Guard.Against.Null(logger, nameof(logger));
            _next =         Guard.Against.Null(next, nameof(next));
            _tests =        Guard.Against.Null(tests, nameof(tests));
            _typeFilter =   Guard.Against.Null(typeFilter, nameof(typeFilter));
            
            Guard.Against.NullOrWhiteSpace(qualityTestRoute, nameof(qualityTestRoute));
            _qualityTestRoute = Guard.Against.InvalidInput(qualityTestRoute, nameof(qualityTestRoute), delegate (string s) { return s.ElementAt(0).Equals('/'); });

            _logger.LogDebug($"Quality Check middleware initialized with {qualityTestRoute}");
        }

        public async Task Invoke(HttpContext context)
        {
            Guard.Against.Null(context?.Request?.Path.Value, nameof(context));

            if (context.Request.Path.Value.ToLower().Contains(_qualityTestRoute.ToLower()))
            {
                _logger.LogDebug($"{nameof(QualityCheckMiddleware)} Running {_tests.Count()} tests");

                var testResults = new List<QualityCheckResponseModel>();

                _tests = ApplyTypeFilter();

                foreach (var test in _tests)
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

        private IEnumerable<IQualityCheck> ApplyTypeFilter()
        {

            if (_typeFilter != null)
            {
                List<IQualityCheck> filteredTests = new List<IQualityCheck>();

                foreach (var test in _tests)
                {
                    var interfaces = test.GetType().GetInterfaces();

                    if (interfaces.Any(i => i.GenericTypeArguments.Contains(_typeFilter)))
                        filteredTests.Add(test);
                }

                return filteredTests;
            }
            else
                return _tests;

        }
    }
}
