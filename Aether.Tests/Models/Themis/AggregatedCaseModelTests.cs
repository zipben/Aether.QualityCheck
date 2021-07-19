using Microsoft.VisualStudio.TestTools.UnitTesting;
using Aether.Models.Themis;
using System;
using System.Collections.Generic;
using System.Text;

namespace Aether.Models.Themis.Tests
{
    [TestClass()]
    public class AggregatedCaseModelTests
    {
        [TestMethod()]
        public void AggregatedCaseModelNameTest()
        {
            var test = Guid.NewGuid().ToString();
            var model = new AggregatedCaseModel();
            model.Name = test;
            Assert.AreEqual(test, model.Name);
        }

        public void AggregatedCaseModelUpdateTimeTest()
        {
            var test = DateTime.Now;
            var model = new AggregatedCaseModel();
            model.UpdateDate = test;
            Assert.AreEqual(test, model.UpdateDate);
        }

        public void AggregatedCaseModelDateHoldCreatedTest()
        {
            var test = DateTime.Now;
            var model = new AggregatedCaseModel();
            model.DateHoldCreated = test;
            Assert.AreEqual(test, model.DateHoldCreated);
        }

        public void AggregatedCaseModelDateHoldEndedTest()
        {
            var test = DateTime.Now;
            var model = new AggregatedCaseModel();
            model.DateHoldEnded = test;
            Assert.AreEqual(test, model.DateHoldEnded);
        }

        public void AggregatedCaseModelIdTest()
        {
            var test = Guid.NewGuid().ToString();
            var model = new AggregatedCaseModel();
            model.Id = test;
            Assert.AreEqual(test, model.Id);
        }

        public void AggregatedCaseModelGcidsTest()
        {
            var test = Guid.NewGuid().ToString();
            var model = new AggregatedCaseModel();
            model.Gcids.Add(test);
            Assert.IsTrue(model.Gcids.Contains(test));
        }

        public void AggregatedCaseModelLoanNumbersTest()
        {
            var test = Guid.NewGuid().ToString();
            var model = new AggregatedCaseModel();
            model.LoanNumbers.Add(test);
            Assert.IsTrue(model.LoanNumbers.Contains(test));
        }

        public void AggregatedCaseModelRMClientIDTest()
        {
            var test = Guid.NewGuid().ToString();
            var model = new AggregatedCaseModel();
            model.RMClientID.Add(test);
            Assert.IsTrue(model.RMClientID.Contains(test));
        }

        public void AggregatedCaseModelRMLoanIdTest()
        {
            var test = Guid.NewGuid().ToString();
            var model = new AggregatedCaseModel();
            model.RMLoanId.Add(test);
            Assert.IsTrue(model.RMLoanId.Contains(test));
        }
    }
}