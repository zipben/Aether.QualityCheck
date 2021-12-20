using Aether.QualityChecks.Attributes;
using Aether.QualityChecks.Helpers;
using Aether.QualityChecks.Interfaces;
using Aether.QualityChecks.Models;
using Ardalis.GuardClauses;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Threading.Tasks;

namespace Aether.QualityChecks.Middleware
{
    public class QualityCheckMiddleware
    {
        private readonly IEnumerable<IQualityCheck> _tests;
        private readonly IQualityCheckExecutionHandler _handler;
        private readonly Type _typeFilter;
        private readonly RequestDelegate _next;

        public QualityCheckMiddleware(RequestDelegate next, IEnumerable<IQualityCheck> tests, IQualityCheckExecutionHandler handler)
        {
            _tests = Guard.Against.Null(tests, nameof(tests));
            _handler = Guard.Against.Null(handler, nameof(handler));
            _next = next;
        }

        public QualityCheckMiddleware(RequestDelegate next, IEnumerable<IQualityCheck> tests, IQualityCheckExecutionHandler handler, Type typeFilter)
        {
            _tests =        Guard.Against.Null(tests, nameof(tests));
            _handler = Guard.Against.Null(handler, nameof(handler));
            _typeFilter =   Guard.Against.Null(typeFilter, nameof(typeFilter));
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            Guard.Against.Null(context, nameof(context));

            var testResults = new List<QualityCheckResponseModel>();

            var filteredTests = ApplyTypeFilter(_tests, _typeFilter);

            foreach (var test in filteredTests)
            {
                testResults.AddRange(await _handler.ExecuteQualityCheck(test));
            }

            context.Response.StatusCode = testResults.All(t => t.CheckPassed) ? (int) HttpStatusCode.OK
                                                                                : (int) HttpStatusCode.InternalServerError;

            context.Response.ContentType = "application/json";
            await context.Response.WriteAsync(JsonConvert.SerializeObject(testResults));
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
