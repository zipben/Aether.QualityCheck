using Aether.Models;
using Aether.Models.ErisClient;
using Aether.Models.Sam;
using Aether.TestUtils.BaseClasses;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Aether.Tests.Models
{
    [TestClass]
    public class ResolutionPathResponseModelTests : ModelUnitTestBase<ResolutionPathResponse>
    {
        [TestMethod]
        public void PageParamsTest()
        {
            BaseModelTest();
        }
    }
}
