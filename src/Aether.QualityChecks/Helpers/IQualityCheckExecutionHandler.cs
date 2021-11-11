using Aether.QualityChecks.Interfaces;
using Aether.QualityChecks.Models;
using System.Threading.Tasks;

namespace Aether.QualityChecks.Helpers
{
    public interface IQualityCheckExecutionHandler
    {
        Task<QualityCheckResponseModel> ExecuteQualityCheck(IQualityCheck qc);
    }
}