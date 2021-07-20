using Aether.Extensions;
using Aether.Interfaces;
using Aether.Models;
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
            IMongoSettings testModelA = AutoFaker.Generate<MongoSettings>();
            IMongoSettings testModelB = testModelA.SluggishClone();
            Assert.AreEqual(testModelA.SluggishHash(), testModelB.SluggishHash());
        }
    }
}
