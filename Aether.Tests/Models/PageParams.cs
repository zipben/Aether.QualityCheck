using Aether.Extensions;
using AutoBogus;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Aether.Tests.Models
{
    [TestClass]
    public class PageParams
    {
        [TestMethod]
        public void PageParamsTest()
        {
            PageParams testModelA = AutoFaker.Generate<PageParams>();
            PageParams testModelB = testModelA.SluggishClone();
            Assert.AreEqual(testModelA.SluggishHash(), testModelB.SluggishHash());
        }
    }
}
