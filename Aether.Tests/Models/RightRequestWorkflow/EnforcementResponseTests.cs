using Aether.Enums;
using Aether.Extensions;
using Aether.Models.Oya;
using Aether.Models.RightRequestWorkflow;
using AutoBogus;
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
        public void EnforcementResponseTest()
        {
            EnforcementResponse testModelA = AutoFaker.Generate<EnforcementResponse>();
            EnforcementResponse testModelB = testModelA.SluggishClone();
            Assert.AreEqual(testModelA.SluggishHash(), testModelB.SluggishHash());
        }
    }
}
