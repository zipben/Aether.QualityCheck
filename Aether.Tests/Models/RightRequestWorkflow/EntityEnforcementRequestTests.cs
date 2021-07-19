using Aether.Enums;
using Aether.Models.RightRequestWorkflow;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;

namespace Aether.Tests.Models.RightRequestWorkflow
{
    [TestClass]
    public class EntityEnforcementRequestTests
    {
        [TestMethod]
        public void EntityEnforcementRequestEnforcementRequestIdTest()
        {
            var target = new EntityEnforcementRequest();
            var test = Guid.NewGuid().ToString();
            target.EnforcementRequestId = test;
            Assert.AreEqual(test, target.EnforcementRequestId);
        }

        [TestMethod]
        public void EntityEnforcementRequestIdentifiersTest()
        {
            var target = new EntityEnforcementRequest();
            target.Identifiers = new Dictionary<string, List<string>>();
            Assert.IsTrue(target.Identifiers.Count == 0);
        }

        [TestMethod]
        public void EntityEnforcementRequestEnforcementTypeTest()
        {
            var target = new EntityEnforcementRequest();
            var test = EnforcementType.RightToDelete;
            target.EnforcementType = test;
            Assert.AreEqual(test, target.EnforcementType);
        }

        [TestMethod]
        public void EntityEnforcementRequestDataPointsRequiredTest()
        {
            var target = new EntityEnforcementRequest();
            var test = Guid.NewGuid().ToString();
            target.DataPointsRequired = new List<string>();
            target.DataPointsRequired.Add(test);
            Assert.IsTrue(target.DataPointsRequired.Contains(test));
        }

        [TestMethod]
        public void EntityEnforcementRequestResponseEndpointTest()
        {
            var target = new EntityEnforcementRequest();
            var test = Guid.NewGuid().ToString();
            target.ResponseEndpoint = test;
            Assert.AreEqual(test, target.ResponseEndpoint);
        }

        [TestMethod]
        public void EntityEnforcementRequestSystemNameTest()
        {
            var target = new EntityEnforcementRequest();
            var test = Guid.NewGuid().ToString();
            target.SystemName = test;
            Assert.AreEqual(test, target.SystemName);
        }
    }
}
