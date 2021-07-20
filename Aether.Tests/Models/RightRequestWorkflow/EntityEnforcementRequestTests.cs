using Aether.Enums;
using Aether.Extensions;
using Aether.Models.RightRequestWorkflow;
using AutoBogus;
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
        public void EntityEnforcementRequest()
        {
            EntityEnforcementRequest testModelA = AutoFaker.Generate<EntityEnforcementRequest>();
            EntityEnforcementRequest testModelB = testModelA.SluggishClone();
            Assert.AreEqual(testModelA.SluggishHash(), testModelB.SluggishHash());
        }
    }
}
