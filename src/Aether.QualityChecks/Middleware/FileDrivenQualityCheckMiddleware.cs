using Aether.QualityChecks.Helpers;
using Aether.QualityChecks.Interfaces;
using Aether.QualityChecks.Models;
using Ardalis.GuardClauses;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace Aether.QualityChecks.Middleware
{
    public class FileDrivenQualityCheckMiddleware
    {
        private readonly IEnumerable<IFileDrivenQualityCheck> _tests;
        private readonly IQualityCheckExecutionHandler _handler;
        private readonly Type _typeFilter;
        private readonly RequestDelegate _next;
        private readonly string _fileName;

        public FileDrivenQualityCheckMiddleware(RequestDelegate next, IEnumerable<IFileDrivenQualityCheck> tests, IQualityCheckExecutionHandler handler,
            string fileName)
        {
            _tests = Guard.Against.Null(tests, nameof(tests));
            _handler = Guard.Against.Null(handler, nameof(handler));
            _fileName = Guard.Against.NullOrEmpty(fileName, nameof(fileName));
            _next = next;
        }

        public FileDrivenQualityCheckMiddleware(RequestDelegate next, IEnumerable<IFileDrivenQualityCheck> tests, IQualityCheckExecutionHandler handler, Type typeFilter,
            string fileName)
        {
            _tests =        Guard.Against.Null(tests, nameof(tests));
            _handler = Guard.Against.Null(handler, nameof(handler));
            _typeFilter =   Guard.Against.Null(typeFilter, nameof(typeFilter));
            _fileName = Guard.Against.NullOrEmpty(fileName, nameof(fileName));
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            Guard.Against.Null(context, nameof(context));
            Guard.Against.InvalidInput(context, nameof(context.Request.Method),
                (context) => { return context.Request.Method.Equals("POST"); }, "File Driven Quality Checks Require Post Requests");

            Guard.Against.NullOrEmpty(context.Request.Form.Files, nameof(context.Request.Form.Files));

            var testFile = await ExtractTestFileFromRequest(context);

            Guard.Against.Null(testFile, nameof(testFile));

            var testResults = new List<QualityCheckResponseModel>();

            var filteredTests = ApplyTypeFilter(_tests, _typeFilter);

            foreach (var test in filteredTests)
            {
                await test.LoadFile(testFile);

                QualityCheckResponseModel response = await _handler.ExecuteQualityCheck(test);
                testResults.Add(response);
            }

            context.Response.StatusCode = testResults.All(t => t.CheckPassed) ? (int)HttpStatusCode.OK
                                                                                : (int)HttpStatusCode.InternalServerError;

            context.Response.ContentType = "application/json";
            await context.Response.WriteAsync(JsonConvert.SerializeObject(testResults));
        }

        private async Task<byte[]> ExtractTestFileFromRequest(HttpContext context)
        {            
            var file = context.Request.Form.Files.First(f => f.FileName.Equals(_fileName));

            using (var memoryStream = new MemoryStream())
            {
                await file.CopyToAsync(memoryStream);
                return memoryStream.ToArray();
            }   
        }

        private static IEnumerable<IFileDrivenQualityCheck> ApplyTypeFilter(IEnumerable<IFileDrivenQualityCheck> tests, Type typeFilter)
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
