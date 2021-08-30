using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Aether.Extensions;
using Aether.ExternalAccessClients;
using Aether.Helpers;
using Aether.Models.NotificationService;
using APILogger.Interfaces;
using Microsoft.Extensions.Options;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace Aether.IntegrationTests
{
    [TestClass]
    public class NotificationMessageHelperTests
    {
        [TestMethod]
        public async Task GenerateEmail()
        {
            Mock<IApiLogger> mockLogger = new Mock<IApiLogger>();
            NotificationMessageHelper notificationMessageHelper = new NotificationMessageHelper(mockLogger.Object);

            HttpClient httpClient = new HttpClient();
            HttpClientWrapper httpClientWrapper = new HttpClientWrapper(httpClient);
            var settings = Options.Create(new NotificationServiceSettings {
                Audience = Environment.GetEnvironmentVariable("NotificationServiceConfig__Audience"),
                ClientID = Environment.GetEnvironmentVariable("NotificationServiceConfig__ClientID"),
                ClientSecret= Environment.GetEnvironmentVariable("NotificationServiceConfig__ClientSecret"),
                BaseUrl= Environment.GetEnvironmentVariable("NotificationServiceConfig__BaseUrl"),
                EndPoint= Environment.GetEnvironmentVariable("NotificationServiceConfig__EndPoint")
            });
            NotificationServiceClient notificationServiceClient = new NotificationServiceClient(httpClientWrapper, settings, mockLogger.Object);

            var bodyParams = new List<KeyValuePair<string, string>>
            {
                new KeyValuePair<string, string>("thisParameter", "I am a body hear me roar")
            };

            var email = notificationMessageHelper.CreateEmail("ZXbcFhmGv-", 
                                                              "test", 
                                                              "206980", 
                                                              "emmanuelaubrey@quickenloans.com", 
                                                              "Hello out there", 
                                                              "emmanuelaubrey@quickenloans.com".CreateList(),
                                                              "emmanuelaubrey@quickenloans.com".CreateList(),
                                                              null,
                                                              bodyParams.ToArray());
            var isSent = await notificationServiceClient.TryPostRequestAsync(email);
            Assert.IsTrue(isSent);
        }
    }
}
