using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading;
using System.Threading.Tasks;
using Aether.ExternalAccessClients;
using APILogger.Interfaces;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Moq.Protected;

namespace Aether.Tests.ExternalAccessClients
{
    [TestClass]
    public class HttpClientWrapperTests
    {
        private HttpClientWrapper _target;
        private Mock<HttpMessageHandler> _mockHttpMessageHandler;
        private Mock<IApiLogger> _mockLogger;
        private HttpClient _httpClient;

        private Uri _testUri;
        private HttpResponseMessage _testResponse;
        private HttpContent _testHttpContent;
        private string _testContentType;

        [TestInitialize]
        public void Init()
        {
            _mockHttpMessageHandler = new Mock<HttpMessageHandler>();
            _mockLogger = new Mock<IApiLogger>();
            _httpClient = new HttpClient(_mockHttpMessageHandler.Object);

            _target = new HttpClientWrapper(_httpClient, _mockLogger.Object);

            SetupTestData();
            SetupMocks();
        }

        private void SetupTestData()
        {
            _testUri = new Uri("http://Test.Uri");
            _testHttpContent = new StringContent(@"[{ ""id"": 1, ""title"": ""Cool post!""}, { ""id"": 100, ""title"": ""Some title""}]");
            _testResponse = new HttpResponseMessage
            {
                StatusCode = HttpStatusCode.OK,
                Content = _testHttpContent,
            };
            _testContentType = "application/json";
        }

        private void SetupMocks()
        {
            _mockHttpMessageHandler.Protected()
                                   .Setup<Task<HttpResponseMessage>>("SendAsync",
                                                                     ItExpr.IsAny<HttpRequestMessage>(),
                                                                     ItExpr.IsAny<CancellationToken>())
                                   .ReturnsAsync(_testResponse);
        }

        [TestMethod]
        public void GetBaseURITest()
        {
            _target.SetBaseURI(_testUri.ToString());

            Assert.AreEqual(_testUri, _target.GetBaseURI());
        }

        [TestMethod]
        public void SetBaseURITest()
        {
            _target.SetBaseURI(_testUri.ToString());

            Assert.AreEqual(_testUri, _httpClient.BaseAddress);
        }

        [TestMethod]
        public async Task DeleteAsyncTest()
        {
            await _target.DeleteAsync(null, _testUri.ToString(), _testHttpContent);

            VerifyResults(HttpMethod.Delete);
        }

        [TestMethod]
        public async Task GetAsyncTest()
        {
            _testHttpContent = null;
            await _target.GetAsync(null, _testUri.ToString());

            VerifyResults(HttpMethod.Get);
        }

        [TestMethod]
        public async Task GetAsyncTest_NoAuth()
        {
            _testHttpContent = null;
            await _target.GetAsync(_testUri.ToString());

            VerifyResults(HttpMethod.Get);
        }

        [TestMethod]
        public async Task PostAsyncTest()
        {
            await _target.PostAsync(null, _testUri.ToString(), _testHttpContent);

            VerifyResults(HttpMethod.Post);
        }

        [TestMethod]
        public async Task PutAsyncTest()
        {
            await _target.PutAsync(null, _testUri.ToString(), _testHttpContent);

            VerifyResults(HttpMethod.Put);
        }

        [TestMethod]
        public async Task PatchAsyncTest()
        {
            await _target.PatchAsync(null, _testUri.ToString(), _testHttpContent);

            VerifyResults(HttpMethod.Patch);
        }

        private void VerifyResults(HttpMethod method)
        {
            _mockHttpMessageHandler.Protected()
                                   .Verify("SendAsync",
                                           Times.Exactly(1),
                                           ItExpr.Is<HttpRequestMessage>(req => req.Method == method 
                                                                             && req.Content == _testHttpContent ),
                                           ItExpr.IsAny<CancellationToken>());
        }

        [TestMethod]
        public void SetContentTypeTest()
        {
            _target.SetContentType(_testContentType);

            Assert.IsTrue(_httpClient.DefaultRequestHeaders.Accept.Contains(new MediaTypeWithQualityHeaderValue(_testContentType)));
        }
    }
}
