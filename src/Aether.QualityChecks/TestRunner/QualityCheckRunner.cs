using Aether.QualityChecks.Attributes;
using Aether.QualityChecks.Helpers;
using Aether.QualityChecks.Interfaces;
using Aether.QualityChecks.Models;
using Ardalis.GuardClauses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Aether.QualityChecks.TestRunner
{
    /// <summary>
    /// A test runner that can be used to trigger the quality checks defined in a service outside of the middleware.
    /// Useful if you are writting tests for something other than an API, and would like a way to trigger your acceptance tests
    /// in ways other than via the middleware
    /// </summary>
    public class QualityCheckRunner : IQualityCheckRunner
    {
        private readonly IEnumerable<IQualityCheck> _tests;
        private readonly IQualityCheckExecutionHandler _handler;

        public QualityCheckRunner(IEnumerable<IQualityCheck> tests, IQualityCheckExecutionHandler handler)
        {
            _tests = Guard.Against.Null(tests, nameof(tests));
            _handler = Guard.Against.Null(handler, nameof(handler));
        }

        public async Task<List<QualityCheckResponseModel>> RunQualityChecks()
        {
            return await RunQualityChecks(filter:null);
        }

        public async Task<List<QualityCheckResponseModel>> RunQualityChecks<T>()
        {
            return await RunQualityChecks(typeof(T));
        }

        private async Task<List<QualityCheckResponseModel>> RunQualityChecks(Type filter = null)
        {
            var testResults = new List<QualityCheckResponseModel>();

            List<IQualityCheck> filteredTests = ApplyTypeFilter(_tests, filter).ToList();

            foreach (var test in filteredTests)
            {
                testResults.AddRange(await _handler.ExecuteQualityCheck(test));
            }

            return testResults;
        }

        private IEnumerable<IQualityCheck> ApplyTypeFilter(IEnumerable<IQualityCheck> tests, Type typeFilter)
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
