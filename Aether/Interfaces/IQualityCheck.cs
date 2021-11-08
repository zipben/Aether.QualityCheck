using Aether.QualityChecks.Models;
using System.Threading.Tasks;

namespace Aether.QualityChecks.Interfaces
{
    public interface IQualityCheck<T> : IQualityCheck { }

    public interface IQualityCheck
    {
        string LogName { get; }
        Task<QualityCheckResponseModel> RunAsync();
        Task TearDownAsync();
    }
}