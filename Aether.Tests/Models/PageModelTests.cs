using Aether.Models;
using Aether.TestUtils.BaseClasses;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Aether.Tests.Models
{
    [TestClass]
    public class PageModelTests : ModelUnitTestBase<PageModel<string>>
    {
        [TestMethod]
        public void PageModelTest()
        {
            BaseModelTest();
        }
    }
}
