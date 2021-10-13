using Aether.Models;
using System;
using System.Threading.Tasks;

namespace Aether.Interfaces
{
    public interface IQualityCheck<T> : IQualityCheck { }

    public interface IQualityCheck
    {
        string LogName { get; }
        Task<QualityCheckResponseModel> RunAsync();
        Task TearDownAsync();
    }
}