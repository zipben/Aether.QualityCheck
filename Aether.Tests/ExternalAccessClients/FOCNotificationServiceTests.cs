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
        private string _testTemplateID;
        private string _testStage;
        private string _testApplicationID;
        private string _testBody;
        private string _testFrom;
        private string _testSubject;
        private List<string> _testTo;
        private List<string> _testCC;


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
            _testTemplateID = "testTemplate";
            _testStage = "teststage";
            _testApplicationID = "00000ID";
            _testBody = "body";
            _testFrom = "from@from.com";
            _testSubject = "subject";
            _testTo = new List<string> { "to@to.com" };
            _testCC = new List<string> { "cc@cc.com" };

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

            _mockNotificationMessageHelper.Verify(x => x.CreateEmail(_testTemplateID, _testStage, _testApplicationID, _testEmailSendModel.From, _testEmailSendModel.Subject, _testEmailSendModel.Body, _testEmailSendModel.To, _testEmailSendModel.CC), Times.Once);

            _mockNotificationServiceClient.Verify(x => x.TryPostRequestAsync(It.IsAny<Models.NotificationServiceEmailBody.EmailRootObject>()), Times.Once);
            _mockNotificationServiceClient.VerifyNoOtherCalls();
        }
    }
}
