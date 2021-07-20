using Microsoft.VisualStudio.TestTools.UnitTesting;
using Aether.Models.Themis;
using System;
using System.Collections.Generic;
using System.Text;
using AutoBogus;
using Aether.Extensions;

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