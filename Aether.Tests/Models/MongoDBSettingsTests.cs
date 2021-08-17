using Aether.Models.Configuration;
using Aether.TestUtils.BaseClasses;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Aether.Tests.Models
{
    [TestClass]
    public class MongoDBSettingsTests : ModelUnitTestBase<MongoDBSettings>
    {
        [TestMethod]
        public void MongoSettingsTest()
        {
            BaseModelTest();
        }
    }
}
