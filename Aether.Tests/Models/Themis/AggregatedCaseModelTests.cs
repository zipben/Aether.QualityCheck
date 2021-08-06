using Aether.Extensions;
using AutoBogus;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Aether.Models.Themis.Tests
{
    [TestClass()]
    public class AggregatedCaseModelTests
    {
        [TestMethod]
        public void AggregatedCaseModelTest()
        {
            AggregatedCaseModel testModelA = AutoFaker.Generate<AggregatedCaseModel>();
            AggregatedCaseModel testModelB = testModelA.SluggishClone();
            Assert.AreEqual(testModelA.SluggishHash(), testModelB.SluggishHash());
        }
    }
}