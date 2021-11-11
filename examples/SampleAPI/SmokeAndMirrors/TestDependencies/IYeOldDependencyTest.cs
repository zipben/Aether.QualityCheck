using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SmokeAndMirrors.TestDependencies
{
    public interface IYeOldDependencyTest
    {
        public Task<bool> FindGoldAsync();
        public Task<bool> DeleteGoldAsync();
    }
}
