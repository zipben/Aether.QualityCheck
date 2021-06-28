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
    }
}
