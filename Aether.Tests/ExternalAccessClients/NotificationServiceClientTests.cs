using Microsoft.VisualStudio.TestTools.UnitTesting;
using Aether.ExternalAccessClients;
using System;
using System.Collections.Generic;
using System.Text;
using Aether.ExternalAccessClients.Interfaces;
using RockLib.OAuth;
using APILogger.Interfaces;
using Aether.Models.NotificationService;
using Moq;
using Microsoft.Extensions.Options;
using static Aether.Models.NotificationService.NotificationServiceEmailBody;
using System.Threading.Tasks;
using System.Net.Http;
using System.Threading;
using System.Linq;
using Aether.Extensions;

namespace Aether.ExternalAccessClients.Tests
{
    [TestClass()]
    public class NotificationServiceClientTests
    {
        private Mock<IHttpClientWrapper> _httpClient;
        private Auth0AuthParams _auth0Auth;
        private Mock<IApiLogger> _apiLogger;
        private NotificationServiceSettings _settings;
        Mock<IOptions<NotificationServiceSettings>> _config;
        private const string AUTHURL = "https://sso.authrock.com/oauth/token";
        private const string _baseURL = "base";
        NotificationServiceClient target;

        [TestInitialize]
        public void init()
        {
            _httpClient = new Mock<IHttpClientWrapper>();
            _auth0Auth = new Auth0AuthParams();
            _apiLogger = new Mock<IApiLogger>();
            _settings = new NotificationServiceSettings();
            _settings.BaseUrl = _baseURL;
            _settings.EndPoint = "end";
            _config = new Mock<IOptions<NotificationServiceSettings>>();
            _config.Setup(x => x.Value).Returns(_settings);
            _apiLogger.Setup(x => x.Method.CallingClassName).Returns("");

            
            HttpResponseMessage response = new HttpResponseMessage(System.Net.HttpStatusCode.OK);

            _httpClient.Setup(x => x.PostAsync(It.IsAny<IAuthParams>(), It.IsAny<string>(), It.IsAny<HttpContent>())).ReturnsAsync(response);

            target = new NotificationServiceClient(_httpClient.Object, _config.Object, _apiLogger.Object);
        }

        [TestMethod()]
        public void NotificationServiceClientTest()
        {
            var notificationServiceClient = new NotificationServiceClient(_httpClient.Object, _config.Object, _apiLogger.Object);
        }

        [TestMethod()]
        public async Task TryPostRequestAsyncTest()
        {
            EmailRootObject emailRootObject = new EmailRootObject();
            var x = await target.TryPostRequestAsync(emailRootObject);
            Assert.AreEqual(x, true);
        }

        [TestMethod()]
        public async Task TryPostRequestAsyncUnsuccessfulTest()
        {
            HttpResponseMessage response = new HttpResponseMessage(System.Net.HttpStatusCode.BadRequest);
            response.Content = "x".GenerateHttpStringContent();

            _httpClient.Setup(x => x.PostAsync(It.IsAny<IAuthParams>(), It.IsAny<string>(), It.IsAny<HttpContent>())).ReturnsAsync(response);

            target = new NotificationServiceClient(_httpClient.Object, _config.Object, _apiLogger.Object);


            EmailRootObject emailRootObject = new EmailRootObject();
            var x = await target.TryPostRequestAsync(emailRootObject);
            Assert.AreEqual(x, false);
        }

        [TestMethod()]
        [ExpectedException(typeof(HttpRequestException))]
        public async Task TryPostRequestAsyncExceptionTest()
        {
            _httpClient.Setup(x => x.PostAsync(It.IsAny<IAuthParams>(), It.IsAny<string>(), It.IsAny<HttpContent>())).Throws(new InvalidOperationException());

            target = new NotificationServiceClient(_httpClient.Object, _config.Object, _apiLogger.Object);


            EmailRootObject emailRootObject = new EmailRootObject();
            var x = await target.TryPostRequestAsync(emailRootObject);
            
        }
    }
}