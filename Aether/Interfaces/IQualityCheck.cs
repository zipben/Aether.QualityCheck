using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Aether.Interfaces
{
    public interface IQualityCheck
    {
        string LogName { get; }
        Task<bool> Run();
        Task Cleanup();
    }
}
