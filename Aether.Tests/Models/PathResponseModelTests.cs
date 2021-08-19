using Aether.Models;
using Aether.Models.ErisClient;
using Aether.Models.SAM;
using Aether.TestUtils.BaseClasses;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Aether.Tests.Models
{
    [TestClass]
    public class PathResponseModelTests : ModelUnitTestBase<PathResponse>
    {
        [TestMethod]
        public void PageParamsTest()
        {
            BaseModelTest();
        }
    }
}
