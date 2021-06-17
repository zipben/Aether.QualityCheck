using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SmokeAndMirrors.TestDependencies
{
    public class YeOldDependencyTest : IYeOldDependencyTest
    {
        public async Task<bool> DeleteGoldAsync()
        {
            return true;
        }

        public async Task<bool> FindGoldAsync()
        {
            return true;
        }
    }
}
