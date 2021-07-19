using Aether.Enums;
using Aether.Models.Oya;
using Aether.Models.RightRequestWorkflow;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;

namespace Aether.Tests.Models.RightRequestWorkflow
{
    [TestClass]
    public class EnforcementResponseTests
    {
        [TestMethod]
        public void EnforcementResponseEnforcementRequestIdTest()
        {
            var target = new EnforcementResponse();
            var test = Guid.NewGuid().ToString();
            target.EnforcementRequestId = test;
            Assert.AreEqual(test, target.EnforcementRequestId);
        }

        [TestMethod]
        public void EnforcementResponseDataTest()
        {
            var target = new EnforcementResponse();
            var test = new Classification();
            test.Category = Guid.NewGuid().ToString();
            target.Data.Add(test);
            Assert.IsTrue(target.Data.Contains(test));
        }

        [TestMethod]
        public void EnforcementResponseRequestCloseReasonTest()
        {
            var target = new EnforcementResponse();
            var test = Guid.NewGuid().ToString();
            target.RequestCloseReason = test;
            Assert.AreEqual(test, target.RequestCloseReason);
        }

        [TestMethod]
        public void EnforcementResponseEnforcementTypeTest()
        {
            var target = new EnforcementResponse();
            var test = EnforcementType.RightToDelete;
            target.EnforcementType = test;
            Assert.AreEqual(test, target.EnforcementType);
        }

        [TestMethod]
        public void EnforcementResponseIsTestMessageTest()
        {
            var target = new EnforcementResponse();
            target.IsTestMessage = true;
            Assert.IsTrue(target.IsTestMessage);
        }
    }
}
