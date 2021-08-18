using Aether.Enums;
using Aether.ExternalAccessClients;
using Aether.ExternalAccessClients.Interfaces;
using Aether.Models.ErisClient;
using Microsoft.Extensions.Options;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Newtonsoft.Json;
using RockLib.OAuth;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Aether.Tests.ExternalAccessClients
{
    [TestClass()]
    public class ErisClientTests
    {
        Mock<IHttpClientWrapper> _mockHttpClientWrapper;
        Mock<IOptions<ErisConfig>> _mockErisConfig;

        [TestInitialize]
        public void Init()
        {
            _mockHttpClientWrapper = new Mock<IHttpClientWrapper>();
            _mockErisConfig = new Mock<IOptions<ErisConfig>>();
        }

        [TestMethod()]
        [ExpectedException(typeof(ArgumentNullException))]
        public void ErisClientTest_NullDependencies()
        {
            ErisClient client = new ErisClient(null, null);
        }

        [TestMethod()]
        [ExpectedException(typeof(ArgumentNullException))]
        public void ErisClientTest_NewedUpDependencies_ThrowNullArgumentException()
        {
            ErisClient client = new ErisClient(_mockHttpClientWrapper.Object, _mockErisConfig.Object);
        }

        [TestMethod()]
        [ExpectedException(typeof(ArgumentNullException))]
        public void ErisClientTest_InitializedErisConfig_NullValues_ThrowNullArgumentException()
        {
            IOptions<ErisConfig> config = Options.Create<ErisConfig>(new ErisConfig());

            ErisClient client = new ErisClient(_mockHttpClientWrapper.Object, config);
        }

        private static IOptions<ErisConfig> GenerateTestConfig()
        {
            return Options.Create<ErisConfig>(new ErisConfig()
            {
                ClientSecret = "IAmASecret",
                ClientID = "IAmAnID",
                Audience = "IAmAnAudience",
                BaseUrl = "IAmABaseUrl"
            });
        }

        [TestMethod()]
        public void ErisClientTest_InitializedErisConfig_NoExceptions()
        {
            IOptions<ErisConfig> config = GenerateTestConfig();

            ErisClient client = new ErisClient(_mockHttpClientWrapper.Object, config);
        }


        [TestMethod()]
        [ExpectedException(typeof(ArgumentNullException))]
        public async Task ErisClientTest_InitializedErisConfig_NullErisRequestModel_ThrowArgumentException()
        {
            IOptions<ErisConfig> config = GenerateTestConfig();

            ErisClient client = new ErisClient(_mockHttpClientWrapper.Object, config);

            await client.ResolveIdentifiersAsync(null);
        }

        [TestMethod()]
        [ExpectedException(typeof(HttpRequestException))]
        public async Task ErisClientTest_InitializedErisConfig_NewedErisRequestModel_OkRequestResponse_NullContent_ThrowsHttpRequestException()
        {
            IOptions<ErisConfig> config = GenerateTestConfig();

            _mockHttpClientWrapper.Setup(h => h.PostAsync(It.IsAny<Auth0AuthParams>(), It.IsAny<string>(), It.IsAny<HttpContent>())).Returns(Task.FromResult(new HttpResponseMessage()));

            ErisClient client = new ErisClient(_mockHttpClientWrapper.Object, config);

            await client.ResolveIdentifiersAsync(new IdentifierRequestModel());
        }

        [TestMethod()]
        [ExpectedException(typeof(HttpRequestException))]
        public async Task ErisClientTest_InitializedErisConfig_NewedErisRequestModel_BadRequestResponse_ThrowsHttpRequestException()
        {
            IOptions<ErisConfig> config = GenerateTestConfig();

            _mockHttpClientWrapper.Setup(h => h.PostAsync(It.IsAny<Auth0AuthParams>(), It.IsAny<string>(), It.IsAny<HttpContent>())).Returns(Task.FromResult(new HttpResponseMessage()
            {
                StatusCode = System.Net.HttpStatusCode.InternalServerError
            }));

            ErisClient client = new ErisClient(_mockHttpClientWrapper.Object, config);

            await client.ResolveIdentifiersAsync(new IdentifierRequestModel());
        }

        [TestMethod()]
        public async Task ErisClientTest_InitializedErisConfig_NewedErisRequestModel_OkRequestResponse_EmptyContent_ReturnsNullResponse()
        {
            IOptions<ErisConfig> config = GenerateTestConfig();

            _mockHttpClientWrapper.Setup(h => h.PostAsync(It.IsAny<Auth0AuthParams>(), It.IsAny<string>(), It.IsAny<HttpContent>())).Returns(Task.FromResult(new HttpResponseMessage()
            {
                Content = new StringContent("")
            }));

            ErisClient client = new ErisClient(_mockHttpClientWrapper.Object, config);

            var response = await client.ResolveIdentifiersAsync(new IdentifierRequestModel());

            Assert.IsNull(response);
        }

        [TestMethod()]
        public async Task ErisClientTest_GetAllPaths_SerializationToValueTupleIsEquivalent(){
            var pathEndpoint = "/api/identifier/paths";
            var paths = new List<(IdentifierType, IdentifierType)>{
                (IdentifierType.ClientName, IdentifierType.CreditGuid),
                (IdentifierType.GCID, IdentifierType.LoanNumber),
            };
            IOptions<ErisConfig> config = GenerateTestConfig();
            _mockHttpClientWrapper.Setup(x => x.GetAsync(pathEndpoint)).ReturnsAsync(new HttpResponseMessage{
                Content = new StringContent(JsonConvert.SerializeObject(paths))
            });

            ErisClient client = new ErisClient(_mockHttpClientWrapper.Object, config);
            var response = await client.GetAllPaths();
            CollectionAssert.AreEquivalent(response, paths);
        }

        [TestMethod()]
        [ExpectedException(typeof(JsonReaderException))]
        public async Task ErisClientTest_InitializedErisConfig_NewedErisRequestModel_OkRequestResponse_InvalidContent_ThrowsJsonReaderException()
        {
            IOptions<ErisConfig> config = GenerateTestConfig();

            _mockHttpClientWrapper.Setup(h => h.PostAsync(It.IsAny<Auth0AuthParams>(), It.IsAny<string>(), It.IsAny<HttpContent>())).Returns(Task.FromResult(new HttpResponseMessage()
            {
                Content = new StringContent("{BADObject}")
            }));

            ErisClient client = new ErisClient(_mockHttpClientWrapper.Object, config);

            var response = await client.ResolveIdentifiersAsync(new IdentifierRequestModel());
        }

        [TestMethod()]
        public async Task ErisClientTest_InitializedErisConfig_NewedErisRequestModel_OkRequestResponse_ValidContent_ReturnsNonNullObject()
        {
            IOptions<ErisConfig> config = GenerateTestConfig();

            IdentifiersRoot testRoot = new IdentifiersRoot();

            _mockHttpClientWrapper.Setup(h => h.PostAsync(It.IsAny<Auth0AuthParams>(), It.IsAny<string>(), It.IsAny<HttpContent>())).Returns(Task.FromResult(new HttpResponseMessage()
            {
                Content = new StringContent(JsonConvert.SerializeObject(testRoot))
            }));

            ErisClient client = new ErisClient(_mockHttpClientWrapper.Object, config);

            var response = await client.ResolveIdentifiersAsync(new IdentifierRequestModel());

            Assert.IsNotNull(response);
        }
    }
}
