using Aether.Extensions;
using Aether.ExternalAccessClients;
using Aether.ExternalAccessClients.Interfaces;
using Aether.Models.Configuration;
using AutoBogus;
using Microsoft.Extensions.Options;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Aether.Tests.ExternalAccessClients
{
    [TestClass]
    public class CreditV2ClientTests
    {
        CreditV2Client _target;
        Mock<IHttpClientWrapper> _mockHttpClient;

        [TestInitialize]
        public void Init()
        {
            _mockHttpClient = new Mock<IHttpClientWrapper>();

            _mockHttpClient.Setup(x => x.GetAsync(It.IsAny<string>())).ReturnsAsync(new System.Net.Http.HttpResponseMessage() { Content = new StringContent(testMismo) });

            CreditV2Configuration config = AutoFaker.Generate<CreditV2Configuration>();

            _target = new CreditV2Client(_mockHttpClient.Object, Options.Create(config));
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void CreditClient_nullDependencies_ArgumentNullException()
        {
            var target = new CreditV2Client(null, null);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void CreditClient_nullConfigurations_ArgumentNullException()
        {
            var target = new CreditV2Client(new HttpClientWrapper(null, null), Options.Create(new Aether.Models.Configuration.CreditV2Configuration()));
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public async Task CreditClient_PullCredit_ArgumentNullException()
        {
            await _target.PullCredit(null);
        }

        [TestMethod]
        public async Task CreditClient_PullCredit()
        {
            string guidString = Guid.NewGuid().ToString();

            await _target.PullCredit(guidString);

            _mockHttpClient.Verify(x => x.GetAsync(It.Is<string>(s => s.EndsWith(guidString))));
        }

    }
}
