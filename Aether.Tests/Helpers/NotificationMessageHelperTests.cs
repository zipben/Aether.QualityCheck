using Aether.Helpers;
using Aether.Models;
using APILogger.Interfaces;
using Microsoft.Extensions.Options;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;

namespace Aether.Tests.Helpers
{
    [TestClass]
    public class NotificationMessageHelperTests
    {
        private NotificationMessageHelper _target;

        private Mock<IApiLogger> _mockApiLogger;

        private static string _testTemplateID = "testTemplate";
        private static string _testStage = "teststage";
        private static string _testApplicationID = "00000ID";
        private static string _testSubject = "I am your loyal subject";
        private static string _testBody = "I am a body don't objectify me";
        private static string _emailGuid = "IamAGuid";
        private static string _testEmail = "MedgarMcGregor@quickenloans.com";
        private static string _testCCEmail = "LuceroToral@quickenloans.com";
        private static string from = "legal@legal.com";
        private static List<string> _testToEmails = new List<string> { _testEmail };
        private static List<KeyValuePair<string, string>> _testBodyParms;
        private static List<string> _testCCEmails = new List<string> { _testCCEmail };
        private static List<string> _testEmptyEmails = new List<string>();

        [TestInitialize]
        public void Init()
        {
            _mockApiLogger = new Mock<IApiLogger>();

            _target = new NotificationMessageHelper(_mockApiLogger.Object);
        }

        [TestMethod]
        public void ArgumentNullExceptionTest()
        {
            Assert.ThrowsException<ArgumentNullException>(() => new NotificationMessageHelper(null));
        }

        [TestMethod]
        public void CreateEmail_AllArguments_ValuesInEmailContent()
        {
            var emailContent = _target.CreateEmail(_testTemplateID, _testStage, _testApplicationID, from, _testSubject, _testBody, _testToEmails, _testCCEmails);

            Assert.AreEqual(_testTemplateID, emailContent.templateId);
            Assert.AreEqual(_testStage, emailContent.stage);
            Assert.AreEqual(_testApplicationID, emailContent.applicationId);
            Assert.AreEqual(_testSubject, emailContent.subjectParameters.messageToReplace);
            Assert.AreEqual(_testBody, emailContent.bodyParameters["thisParamater"]);
            Assert.AreEqual(from, emailContent.sendParameters.from);

            Assert.IsTrue(Array.Exists(emailContent.sendParameters.to, item => item == _testEmail));
            Assert.IsTrue(Array.Exists(emailContent.sendParameters.cc, item => item == _testCCEmail));

        }

        [TestMethod]
        public void CreateEmail_AllArguments_ValuesInEmailContent_WithNullCC()
        {
            var emailContent = _target.CreateEmail(_testTemplateID, _testStage, _testApplicationID, from, _testSubject, _testBody, _testToEmails);

            Assert.AreEqual(_testTemplateID, emailContent.templateId);
            Assert.AreEqual(_testStage, emailContent.stage);
            Assert.AreEqual(_testApplicationID, emailContent.applicationId);
            Assert.AreEqual(_testSubject, emailContent.subjectParameters.messageToReplace);
            Assert.AreEqual(_testBody, emailContent.bodyParameters["thisParamater"]);
            Assert.AreEqual(from, emailContent.sendParameters.from);
            Assert.AreEqual(null, emailContent.sendParameters.cc);

            Assert.IsTrue(Array.Exists(emailContent.sendParameters.to, item => item == _testEmail));
        }

        [TestMethod]
        public void CreateEmail_AllArguments_ValuesInEmailContent_WithNullCCAndMultipleBodyParams()
        {
            _testBodyParms = new List<KeyValuePair<string, string>>();
            _testBodyParms.Add(new KeyValuePair<string, string>("thisParamater", _testBody));
            _testBodyParms.Add(new KeyValuePair<string, string>("EmailConfirmationId", _emailGuid));
            var emailContent = _target.CreateEmail(_testTemplateID, _testStage, _testApplicationID, from, _testSubject, _testToEmails, null, _testBodyParms.ToArray());

            Assert.AreEqual(_testTemplateID, emailContent.templateId);
            Assert.AreEqual(_testStage, emailContent.stage);
            Assert.AreEqual(_testApplicationID, emailContent.applicationId);
            Assert.AreEqual(_testSubject, emailContent.subjectParameters.messageToReplace);
            Assert.AreEqual(_testBody, emailContent.bodyParameters["thisParamater"]);
            Assert.AreEqual(_emailGuid, emailContent.bodyParameters["EmailConfirmationId"]);
            Assert.AreEqual(from, emailContent.sendParameters.from);
            Assert.AreEqual(null, emailContent.sendParameters.cc);

            Assert.IsTrue(Array.Exists(emailContent.sendParameters.to, item => item == _testEmail));
        }

        public static IEnumerable<object[]> CreateEmailTest_ArgumentNullExceptionData()
        {
            yield return new object[] { null, _testStage, _testApplicationID, from, _testSubject, _testBody, _testToEmails, _testCCEmails };
            yield return new object[] { _testTemplateID, null, _testApplicationID, from, _testSubject, _testBody, _testToEmails, _testCCEmails };
            yield return new object[] { _testTemplateID, _testStage, null, from, _testSubject, _testBody, _testToEmails, _testCCEmails };
            yield return new object[] { _testTemplateID, _testStage, _testApplicationID, null, _testSubject, _testBody, _testToEmails, _testCCEmails };
            yield return new object[] { _testTemplateID, _testStage, _testApplicationID, from, null, _testBody, _testToEmails, _testCCEmails };
            yield return new object[] { _testTemplateID, _testStage, _testApplicationID, from, _testSubject, _testBody, null, _testCCEmails };

        }
        [TestMethod]
        [DynamicData(nameof(CreateEmailTest_ArgumentNullExceptionData), DynamicDataSourceType.Method)]
        public void CreateEmailTest_ArgumentNullException(string templateId, string stage, string applicationId, string from, string subject, string body, List<string> toEmails, List<string> ccEmails)
        {
            Assert.ThrowsException<ArgumentNullException>(() => _target.CreateEmail(templateId, stage, applicationId, from, subject, body, toEmails, ccEmails));
        }

        public static IEnumerable<object[]> CreateEmailTest_ArgumentExceptionData()
        {
            yield return new object[] { _testTemplateID, _testStage, _testApplicationID, from, _testSubject, null, _testToEmails, _testCCEmails };
        }
        [TestMethod]
        [DynamicData(nameof(CreateEmailTest_ArgumentExceptionData), DynamicDataSourceType.Method)]
        public void CreateEmailTest_ArgumentException(string templateId, string stage, string applicationId, string from, string subject, string body, List<string> toEmails, List<string> ccEmails)
        {
            Assert.ThrowsException<ArgumentException>(() => _target.CreateEmail(templateId, stage, applicationId, from, subject, body, toEmails, ccEmails));
        }
    }
}
