using Aether.QualityChecks.Interfaces;
using Aether.QualityChecks.Models;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Aether.QualityChecks.Helpers
{
    public interface IQualityCheckExecutionHandler
    {
        Task<List<QualityCheckResponseModel>> ExecuteQualityCheck(IQualityCheck qc);
    }
}