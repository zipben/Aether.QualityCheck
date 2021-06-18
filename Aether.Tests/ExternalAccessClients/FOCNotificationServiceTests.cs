using Aether.ExternalAccessClients;
using Aether.ExternalAccessClients.Interfaces;
using Aether.Helpers.Interfaces;
using Aether.Models;
using APILogger.Interfaces;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Aether.Tests.ExternalAccessClients
{
    [TestClass]
    public class FOCNotificationServiceTests
    {
        private FOCNotificationService _target;

        private Mock<IApiLogger> _apiLogger;
        private Mock<INotificationServiceClient> _mockNotificationServiceClient;
        private Mock<INotificationMessageHelper> _mockNotificationMessageHelper;
        private EmailSendModel _testEmailSendModel;
        private EmailSendModel _testEmailSendModelWithNullCC;
        private static string _testTemplateID = "testTemplate";
        private static string _testStage = "teststage";
        private static string _testApplicationID = "00000ID";
        private static string _testBody = "body";
        private static string _testFrom = "from@from.com";
        private static string _testSubject = "subject";
        private static List<string> _testTo = new List<string> { "to@to.com" };
        private static List<string> _testCC = new List<string> { "cc@cc.com" };
        

        [TestInitialize]
        public void Init()
        {
            _apiLogger = new Mock<IApiLogger>();
            _mockNotificationServiceClient = new Mock<INotificationServiceClient>();
            _mockNotificationMessageHelper = new Mock<INotificationMessageHelper>();

            _target = new FOCNotificationService(_apiLogger.Object, _mockNotificationServiceClient.Object, _mockNotificationMessageHelper.Object);

            SetupTestData();
        }

        private void SetupTestData()
        {
            _testEmailSendModel = new EmailSendModel
            {
                TemplateId = _testTemplateID,
                Stage = _testStage,
                ApplicationId = _testApplicationID,
                Body = _testBody,
                From = _testFrom,
                Subject = _testSubject,
                To = _testTo,
                CC = _testCC
            };
            _testEmailSendModelWithNullCC = new EmailSendModel
            {
                TemplateId = _testTemplateID,
                Stage = _testStage,
                ApplicationId = _testApplicationID,
                Body = _testBody,
                From = _testFrom,
                Subject = _testSubject,
                To = _testTo,
            };
        }

        [TestMethod]
        public void Constructor_NullClient_ReturnArgumentNullException()
        {
            Assert.ThrowsException<ArgumentNullException>(() => new FOCNotificationService(null, null, null));
        }

        [TestMethod]
        public async Task SendEmailAsync()
        {
            await _target.SendEmailAsync(_testEmailSendModel);

            _mockNotificationMessageHelper.Verify(x => x.CreateEmail(_testEmailSendModel.TemplateId, _testEmailSendModel.Stage, _testEmailSendModel.ApplicationId, _testEmailSendModel.From, _testEmailSendModel.Subject, _testEmailSendModel.Body, _testEmailSendModel.To, _testEmailSendModel.CC), Times.Once);

            _mockNotificationServiceClient.Verify(x => x.TryPostRequestAsync(It.IsAny<NotificationServiceEmailBody.EmailRootObject>()), Times.Once);
            _mockNotificationServiceClient.VerifyNoOtherCalls();
        }

        [TestMethod]
        public async Task SendEmailAsyncWithNullCC()
        {
            await _target.SendEmailAsync(_testEmailSendModelWithNullCC);

            _mockNotificationMessageHelper.Verify(x => x.CreateEmail(_testEmailSendModelWithNullCC.TemplateId, _testEmailSendModelWithNullCC.Stage, _testEmailSendModelWithNullCC.ApplicationId, _testEmailSendModelWithNullCC.From, _testEmailSendModelWithNullCC.Subject, _testEmailSendModelWithNullCC.Body, _testEmailSendModelWithNullCC.To, _testEmailSendModelWithNullCC.CC), Times.Once);

            _mockNotificationServiceClient.Verify(x => x.TryPostRequestAsync(It.IsAny<NotificationServiceEmailBody.EmailRootObject>()), Times.Once);
            _mockNotificationServiceClient.VerifyNoOtherCalls();
        }

        public static IEnumerable<object[]> SendEmailAsyncTest_ArgumentNullExceptionData()
        {
            yield return new object[] { null, _testStage, _testApplicationID, _testFrom, _testSubject, _testBody, _testTo, _testCC };
            yield return new object[] { _testTemplateID, null, _testApplicationID, _testFrom, _testSubject, _testBody, _testTo, _testCC };
            yield return new object[] { _testTemplateID, _testStage, null, _testFrom, _testSubject, _testBody, _testTo, _testCC };
            yield return new object[] { _testTemplateID, _testStage, _testApplicationID, null, _testSubject, _testBody, _testTo, _testCC };
            yield return new object[] { _testTemplateID, _testStage, _testApplicationID, _testFrom, null, _testBody, _testTo, _testCC };
            yield return new object[] { _testTemplateID, _testStage, _testApplicationID, _testFrom, _testSubject, null, _testTo, _testCC};
            yield return new object[] { _testTemplateID, _testStage, _testApplicationID, _testFrom, _testSubject, _testBody, null, _testCC };
        }

        [TestMethod]
        [DynamicData(nameof(SendEmailAsyncTest_ArgumentNullExceptionData), DynamicDataSourceType.Method)]
        public async Task SendEmailAsyncTest_ArgumentNullException(string templateId, string stage, string applicationId, string from, string subject, string body, List<string> toEmails, List<string> ccEmails)
        {
            await Assert.ThrowsExceptionAsync<ArgumentNullException>(() => _target.SendEmailAsync(new EmailSendModel
            {
                TemplateId = templateId,
                Stage = stage,
                ApplicationId = applicationId,
                Body = body,
                From = from,
                Subject = subject,
                To = toEmails,
                CC = ccEmails
            }));
        }
    }
}
