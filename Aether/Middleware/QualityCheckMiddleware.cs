﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Aether.Interfaces;
using Aether.Models;
using APILogger.Interfaces;
using Ardalis.GuardClauses;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

namespace Aether.Middleware
{
    public class QualityCheckMiddleware : MiddlewareBase
    {
        private readonly string _qualityTestRoute;
        
        private readonly IEnumerable<IQualityCheck> _tests;

        public QualityCheckMiddleware(IApiLogger logger, RequestDelegate next, IEnumerable<IQualityCheck> tests, string qualityTestRoute) : base(logger, next)
        {
            _tests = Guard.Against.Null(tests, nameof(tests));
            
            Guard.Against.NullOrWhiteSpace(qualityTestRoute, nameof(qualityTestRoute));
            _qualityTestRoute = Guard.Against.InvalidInput(qualityTestRoute, nameof(qualityTestRoute), delegate (string s) { return s.ElementAt(0).Equals('/'); });

            _logger.LogDebug($"{nameof(QualityCheckMiddleware)} initialized with {qualityTestRoute}");
        }

        public async Task Invoke(HttpContext context)
        {
            Guard.Against.Null(context?.Request?.Path.Value, nameof(context));

            if (context.Request.Path.Value.Contains(_qualityTestRoute))
            {
                _logger.LogDebug($"{nameof(QualityCheckMiddleware)} Running {_tests.Count()} tests");

                var testResults = new List<QualityCheckResponseModel>();

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
    }
}
