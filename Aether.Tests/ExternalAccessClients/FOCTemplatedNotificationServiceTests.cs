using Microsoft.VisualStudio.TestTools.UnitTesting;
using Aether.ExternalAccessClients;
using System;
using System.Collections.Generic;
using System.Text;
using APILogger.Interfaces;
using Aether.ExternalAccessClients.Interfaces;
using Aether.Helpers.Interfaces;
using Moq;
using static Aether.Models.NotificationService.NotificationServiceEmailBody;
using Aether.Models.NotificationService;
using System.Threading.Tasks;

namespace Aether.ExternalAccessClients.Tests
{
    [TestClass()]
    public class FOCTemplatedNotificationServiceTests
    {
        private Mock<IApiLogger> _apiLogger;
        private Mock<INotificationServiceClient> _notificationServiceClient;
        private Mock<INotificationMessageHelper> _notificationMessageHelper;
        private FOCTemplatedNotificationService _target;

        [TestInitialize]
        public void Init()
        {
            _apiLogger = new Mock<IApiLogger>();
            _notificationServiceClient = new Mock<INotificationServiceClient>();
            _notificationMessageHelper = new Mock<INotificationMessageHelper>();
            MockSetup();
            _target = new FOCTemplatedNotificationService(_apiLogger.Object, _notificationServiceClient.Object, _notificationMessageHelper.Object);
        }

        public void MockSetup()
        {
            _notificationServiceClient.Setup(x => x.TryPostRequestAsync(It.IsAny<EmailRootObject>())).ReturnsAsync(true);
        }

        [TestMethod()]
        [ExpectedException(typeof(ArgumentNullException))]
        public async Task SendEmailAsyncEmptyTest()
        {
            var email = new TemplatedEmailSendModel();
            var x = await _target.SendEmailAsync(email);
            Assert.IsTrue(x);

        }

        [TestMethod()]
        public async Task SendEmailAsyncTest()
        {
            var email = new TemplatedEmailSendModel();
            email.Contents = new Dictionary<string, string>();
            email.Contents.Add(Guid.NewGuid().ToString(), Guid.NewGuid().ToString());
            email.TemplateId = Guid.NewGuid().ToString();
            email.Stage = Guid.NewGuid().ToString();
            email.ApplicationId = Guid.NewGuid().ToString();
            email.From = Guid.NewGuid().ToString();
            email.Subject = Guid.NewGuid().ToString();
            var toList = new List<string>();
            toList.Add(Guid.NewGuid().ToString());
            email.To = toList;
            var x = await _target.SendEmailAsync(email);
            Assert.IsTrue(x);
        }
    }
}