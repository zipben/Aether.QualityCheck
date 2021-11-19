using Aether.QualityChecks.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Aether.QualityChecks.TestRunner
{
    /// <summary>
    /// A test runner that can be used to trigger the quality checks defined in a service outside of the middleware.
    /// Useful if you are writting tests for something other than an API, and would like a way to trigger your acceptance tests
    /// in ways other than via the middleware
    /// </summary>
    public interface IQualityCheckRunner
    {
        Task<List<QualityCheckResponseModel>> RunQualityChecks<T>();
        Task<List<QualityCheckResponseModel>> RunQualityChecks();
    }
}