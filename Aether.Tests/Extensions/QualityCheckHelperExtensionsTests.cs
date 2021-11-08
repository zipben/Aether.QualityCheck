using Aether.QualityChecks.Extensions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;

namespace Aether.Extensions.Tests
{
    [TestClass]
    public class QualityCheckHelperExtensionsTests
    {
        private const string CHECK_NAME = "CheckName";
        private const string STEP_NAME = "StepName";

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void QuickConvertToModelTest_NullParams()
        {
            true.QuickConvertToQualityCheckResponse(null, null);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void QuickConvertToModelTest_NullStepName()
        {
            true.QuickConvertToQualityCheckResponse(CHECK_NAME, null);
        }

        [TestMethod]
        public void QuickConvertToModelTest_CheckModel_True()
        {
            var model = true.QuickConvertToQualityCheckResponse(CHECK_NAME, STEP_NAME);

            Assert.AreEqual(model.Name, CHECK_NAME);
            Assert.IsTrue(model.CheckPassed);
            Assert.IsTrue(model.Steps.Any());
            Assert.IsTrue(model.Steps.First().StepPassed);
            Assert.AreEqual(model.Steps.First().Name, STEP_NAME);
        }

        [TestMethod]
        public void QuickConvertToModelTest_CheckModel_False()
        {
            var model = false.QuickConvertToQualityCheckResponse(CHECK_NAME, STEP_NAME);

            Assert.AreEqual(model.Name, CHECK_NAME);
            Assert.IsFalse(model.CheckPassed);
            Assert.IsTrue(model.Steps.Any());
            Assert.IsFalse(model.Steps.First().StepPassed);
            Assert.AreEqual(model.Steps.First().Name, STEP_NAME);
        }
    }
}