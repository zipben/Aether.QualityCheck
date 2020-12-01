using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Aether.Enums;
using Aether.ExternalAccessClients;
using Aether.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Moq.Protected;
using Newtonsoft.Json;

namespace Aether.Tests.ExternalAccessClients
{

    [TestClass]
    public class HttpClientProviderTests
    {
        private class TestHttpProvider : HttpClientProvider
        {
            public TestHttpProvider(HttpClient httpClient) : base(httpClient) { }
            
            public new async Task<string> GetToken(ServiceSettings serviceSettings, string scope) => 
                await base.GetToken(serviceSettings, scope);

            public new HttpRequestMessage CreateHttpRequestMessage(string url, HttpMethod method, List<KeyValuePair<string, string>> headers, AuthenticationHeaderValue auth, HttpContent content) =>
                base.CreateHttpRequestMessage(url, method, headers, auth, content);

            public new HttpRequestMessage CreateHttpRequestMessage(Uri uri, HttpMethod method, List<KeyValuePair<string, string>> headers, AuthenticationHeaderValue auth, HttpContent content) =>
                base.CreateHttpRequestMessage(uri, method, headers, auth, content);
        }

        TestHttpProvider _target;

        private Mock<HttpMessageHandler> _mockHttpMessageHandler;
        private HttpClient _httpClient;

        private ServiceSettings _testServiceSettings;
        private string _testScope;
        private string _testToken;
        private HttpResponseMessage _testTokenHttpResponseMessage;
        private List<KeyValuePair<string, string>> _testHeaders;
        private AuthenticationHeaderValue _testAuthenticationHeaderValue;
        private Mock<HttpContent> _mockHttpContent;

        [TestInitialize]
        public void Init()
        {
            _mockHttpMessageHandler = new Mock<HttpMessageHandler>();
            _httpClient = new HttpClient(_mockHttpMessageHandler.Object);
            _target = new TestHttpProvider(_httpClient);
            _mockHttpContent = new Mock<HttpContent>();

            SetupTestData();
            SetupMocks();
        }

        private void SetupTestData()
        {
            _testServiceSettings = new ServiceSettings
            {
                SendParametersInQueryString = true,
                AuthorizerUrl = "http://auth.uri/",
                UseBasicAuthentication = true,
                BaseUrl = "http://url.uri/",
                ClientId = Guid.NewGuid().ToString(),
                GrantType = GrantType.Client_Credentials,
                Password = Guid.NewGuid().ToString(),
                UserName = Guid.NewGuid().ToString()
            };
            _testScope = Guid.NewGuid().ToString();

            _testToken = Guid.NewGuid().ToString();

            _testTokenHttpResponseMessage = new HttpResponseMessage
            {
                StatusCode = HttpStatusCode.OK,
                Content = new StringContent(JsonConvert.SerializeObject(new { access_token = _testToken }))
            };

            _testHeaders = new List<KeyValuePair<string, string>>();
            for (var i = 0; i < 10; i++)
            {
                _testHeaders.Add(new KeyValuePair<string, string>(Guid.NewGuid().ToString(), Guid.NewGuid().ToString()));
            }

            _testAuthenticationHeaderValue = new AuthenticationHeaderValue("Scheme");
        }

        private void SetupMocks()
        {
            _mockHttpMessageHandler.Protected()
                                   .Setup<Task<HttpResponseMessage>>("SendAsync",
                                                                     ItExpr.Is<HttpRequestMessage>(req => req.Method == HttpMethod.Post),
                                                                     ItExpr.IsAny<CancellationToken>())
                                   .ReturnsAsync(_testTokenHttpResponseMessage);
        }

        [TestMethod]
        public async Task GetTokenTest()
        {
            var actualToken = await _target.GetToken(_testServiceSettings, _testScope);

            Assert.AreEqual(_testToken, actualToken);
        }

        [TestMethod]
        public void CreateHttpRequestMessageTest_Url()
        {
            var actualMessage = _target.CreateHttpRequestMessage(_testServiceSettings.BaseUrl, HttpMethod.Post, _testHeaders, _testAuthenticationHeaderValue, _mockHttpContent.Object);
            ValidateCreateHttpRequestMessageTest(actualMessage);
        }

        [TestMethod]
        public void CreateHttpRequestMessageTest_Uri()
        {
            var actualMessage = _target.CreateHttpRequestMessage(new Uri(_testServiceSettings.BaseUrl), HttpMethod.Post, _testHeaders, _testAuthenticationHeaderValue, _mockHttpContent.Object);
            ValidateCreateHttpRequestMessageTest(actualMessage);
        }

        private void ValidateCreateHttpRequestMessageTest(HttpRequestMessage actualMessage)
        {
            Assert.AreEqual(_mockHttpContent.Object, actualMessage.Content);
            Assert.AreEqual(HttpMethod.Post, actualMessage.Method);
            Assert.AreEqual(_testServiceSettings.BaseUrl, actualMessage.RequestUri.ToString());

            foreach (var header in _testHeaders)
            {
                Assert.IsTrue(actualMessage.Headers.Contains(header.Key));
                Assert.AreEqual(header.Value, actualMessage.Headers.GetValues(header.Key).ToList().Single());
            }
        }
    }
}
