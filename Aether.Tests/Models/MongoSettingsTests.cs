using Aether.Extensions;
using Aether.Interfaces.Configuration;
using Aether.Models.Configuration;
using AutoBogus;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Aether.Tests.Models
{
    [TestClass]
    public class MongoSettingsTests
    {
        [TestMethod]
        public void MongoSettingsTest()
        {
            MongoDBSettings testModelA = AutoFaker.Generate<MongoDBSettings>();
            IMongoDBSettings testModelB = testModelA.SluggishClone();
            Assert.AreEqual(testModelA.SluggishHash(), testModelB.SluggishHash());
        }
    }
}
