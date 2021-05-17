﻿using Aether.Interfaces;
using APILogger.Interfaces;
using Ardalis.GuardClauses;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

using static Aether.Extensions.MethodExtensions;

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

                var testResults = new List<bool>();

                foreach (var test in _tests)
                {
                    bool isSuccessful = false;

                    try
                    {
                        _logger.LogInfo($"running {test.LogName}");

                        isSuccessful = await test.Run();
                    }
                    catch(Exception e)
                    {
                        _logger.LogError(test.LogName, exception: e);
                    }
                    finally
                    {
                        string status = isSuccessful ? "Passed" : "Failed";

                        _logger.LogInfo($"{test.LogName} status: {status}");

                        testResults.Add(isSuccessful);
                    }
                }

                var passedTests = testResults.Where(t => t == true).Count();
                var failedTests = testResults.Where(t => t == false).Count();

                _logger.LogDebug($"{nameof(QualityCheckMiddleware)}: Tests Passed: {passedTests}, Tests Failed: {failedTests}");

                if (testResults.Any(t => t == false))
                {
                    context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                }
                else
                { 
                    context.Response.StatusCode = (int)HttpStatusCode.OK;
                }
            }
            else
            {
                await _next(context);
            }
        }
    }
}