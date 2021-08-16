using Aether.Extensions;
using Aether.Models;
using AutoBogus;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Aether.Tests.Models
{
    [TestClass]
    public class PageModel
    {
        [TestMethod]
        public void PageModelTest()
        {
            PageModel<string> testModelA = AutoFaker.Generate<PageModel<string>>();
            PageModel<string> testModelB = testModelA.SluggishClone();
            Assert.AreEqual(testModelA.SluggishHash(), testModelB.SluggishHash());
        }
    }
}
