using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Aether.Interfaces
{
    public interface IQualityCheck
    {
        Task<bool> Run();
    }
}
