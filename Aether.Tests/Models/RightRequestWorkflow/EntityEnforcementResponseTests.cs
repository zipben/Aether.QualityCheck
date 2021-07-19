using Aether.Enums;
using Aether.Models.RightRequestWorkflow;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;

namespace Aether.Tests.Models.RightRequestWorkflow
{
    [TestClass]
    public class EntityEnforcementResponseTests
    {
        [TestMethod]
        public void EntityEnforcementRequestEnforcementRequestIdTest()
        {
            var target = new EntityEnforcementResponse();
            var test = Guid.NewGuid().ToString();
            target.EnforcementRequestId = test;
            Assert.AreEqual(target.EnforcementRequestId, test);
        }

        [TestMethod]
        public void EntityEnforcementRequestSendingSystemNameTest()
        {
            var target = new EntityEnforcementResponse();
            var test = Guid.NewGuid().ToString();
            target.SendingSystemName = test;
            Assert.AreEqual(target.SendingSystemName, test);
        }

        [TestMethod]
        public void EntityEnforcementRequestOwnedDataTest()
        {
            var target = new EntityEnforcementResponse();
            target.OwnedData = new Dictionary<string, List<string>>();
            Assert.IsTrue(target.OwnedData.Count == 0);
        }

        [TestMethod]
        public void EntityEnforcementRequestEnforcementTypeTest()
        {
            var target = new EntityEnforcementResponse();
            var test = EnforcementType.RightToDelete;
            target.EnforcementType = test;
            Assert.AreEqual(target.EnforcementType, test);
        }
    }
}
