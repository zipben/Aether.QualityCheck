using Aether.Extensions;
using Aether.Models.RightRequestWorkflow;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Aether.Tests.Models
{
    [TestClass]
    public class EnforcementRequestModelTests
    {
        [TestMethod]
        public void EnforcementRequest_DiagnosticTags_IsTestFlag_True()
        {
            EnforcementRequest model = new EnforcementRequest()
            {
                IsTestMessage = true
            };

            Assert.IsTrue(model.DiagnosticFlags.Contains("IsTest"));
        }

        [TestMethod]
        public void EnforcementRequest_DiagnosticTags_IsTestFlag_False()
        {
            EnforcementRequest model = new EnforcementRequest()
            {
                IsTestMessage = false
            };

            Assert.IsTrue(!model.DiagnosticFlags.Contains("IsTest"));
        }

        [TestMethod]
        public void EnforcementRequest_IsTest_Serialization()
        {
            EnforcementRequest model = new EnforcementRequest()
            {
                IsTestMessage = true
            };

            //Sluggish clone uses seriliazation 
            var modelClone = model.SluggishClone();

            Assert.IsTrue(modelClone.DiagnosticFlags.Contains("IsTest"));
        }

        [TestMethod]
        public void EnforcementRequest_DiagnosticTags_Serialization()
        {
            EnforcementRequest model = new EnforcementRequest();
            model.DiagnosticFlags.Add("ShowTime");
              
            //Sluggish clone uses seriliazation 
            var modelClone = model.SluggishClone();

            Assert.IsTrue(modelClone.DiagnosticFlags.Contains("ShowTime"));
        }

    }
}
