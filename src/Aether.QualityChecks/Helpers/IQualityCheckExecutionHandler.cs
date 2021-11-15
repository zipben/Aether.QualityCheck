using Aether.QualityChecks.Interfaces;
using Aether.QualityChecks.Models;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace Aether.QualityChecks.Helpers
{
    public interface IQualityCheckExecutionHandler
    {
        Task<QualityCheckResponseModel> ExecuteQualityCheck<T>(T qc, HttpRequest request);
    }
}