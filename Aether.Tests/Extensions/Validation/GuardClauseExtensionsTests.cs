using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using Ardalis.GuardClauses;
using Microsoft.Extensions.Configuration;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace Aether.Tests.Extensions.Validation
{
    [TestClass]
    public class GuardClauseExtensionsTests
    {
        [TestMethod]
        public void MissingKeyTest()
        {
            var testDictionary = new Dictionary<string, string>
            {
                { "TestKey", "fun" }
            };

            Guard.Against.MissingKey(testDictionary, "TestKey", "ParamName");
        }

        [TestMethod]
        public void MissingKeyTest_ArgumentNullException()
        {
            var testDictionary = new Dictionary<string, string>();

            Assert.ThrowsException<ArgumentNullException>(() => Guard.Against.MissingKey(testDictionary, "TestKey", "ParamName"));
        }


        [TestMethod]
        [DataRow(HttpStatusCode.OK)]
        [DataRow(HttpStatusCode.NoContent)]
        [DataRow(HttpStatusCode.Accepted)]
        [DataRow(HttpStatusCode.AlreadyReported)]
        [DataRow(HttpStatusCode.Created)]
        [DataRow(HttpStatusCode.NonAuthoritativeInformation)]
        [DataRow(HttpStatusCode.PartialContent)]
        public void UnsuccessfulHttpRequestTest(HttpStatusCode code)
        {
            Guard.Against.UnsuccessfulHttpRequest(MakeHttpResponseMessage(code));
        }

        [TestMethod]
        [DataRow(HttpStatusCode.BadRequest)]
        [DataRow(HttpStatusCode.NotFound)]
        [DataRow(HttpStatusCode.InternalServerError)]
        [DataRow(HttpStatusCode.ServiceUnavailable)]
        [DataRow(HttpStatusCode.GatewayTimeout)]
        [DataRow(HttpStatusCode.Forbidden)]
        [DataRow(HttpStatusCode.MethodNotAllowed)]
        [DataRow(HttpStatusCode.NotAcceptable)]
        public void UnsuccessfulHttpRequestTest_HttpRequestException(HttpStatusCode code)
        {
            Assert.ThrowsException<HttpRequestException>(() => Guard.Against.UnsuccessfulHttpRequest(MakeHttpResponseMessage(code)));
        }

        private HttpResponseMessage MakeHttpResponseMessage(HttpStatusCode code) =>
            new HttpResponseMessage(code)
            {
                RequestMessage = new HttpRequestMessage(HttpMethod.Get, "Http://tempuri.org")
            };

        [TestMethod]
        public void MissingConfigurationSectionTest()
        {
            var config = new Mock<IConfiguration>().Object;
            Assert.ThrowsException<ArgumentNullException>(() => Guard.Against.MissingConfigurationSection(config, Guid.NewGuid().ToString()));
        }
        [TestMethod]
        public void MissingConfigurationSectionExistsTest()
        {
            var config = ConfigurationSetup();
            Assert.AreEqual(Guard.Against.MissingConfigurationSection(config, Guid.NewGuid().ToString()), config.GetSection("any"));
        }

        private IConfiguration ConfigurationSetup()
        {
            var configSection = ConfigurationSectionSetup();
            var config = new Mock<IConfiguration>();
            config.Setup(x => x.GetSection(It.IsAny<string>())).Returns(configSection);
            return config.Object;
        }
        private IConfigurationSection ConfigurationSectionSetup()
        {
            var config = new Mock<IConfigurationSection>();
            config.Setup(x => x.Key).Returns("key");
            config.Setup(x => x.Value).Returns("value");
            return config.Object;
        }

        [TestMethod]
        public void MissingConfigurationValueHasValueTest()
        {
            var configSection = ConfigurationSectionSetup();

            Assert.AreEqual(Guard.Against.MissingConfigurationValue(configSection), configSection.Value);
        }

        [TestMethod]
        public void MissingConfigurationValueNullValueTest()
        {
            var configSection = new Mock<IConfigurationSection>().Object;

            Assert.ThrowsException<ArgumentException>(() => Guard.Against.MissingConfigurationValue(configSection));
        }

        [TestMethod]
        public void MissingConfigurationValueConfigurationHasValueTest()
        {
            var config = ConfigurationSetup();

            Assert.AreEqual(Guard.Against.MissingConfigurationValue(config, "key"), ConfigurationSectionSetup().Value) ;
        }
    }
}
