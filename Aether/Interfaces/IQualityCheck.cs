using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Aether.Interfaces
{
    public interface IQualityCheck
    {
        int Order { get; }
        Task<bool> Run();
    }
}
