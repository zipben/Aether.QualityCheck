using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using SmokeAndMirrors;
using SmokeAndMirrors.TestDependencies;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Aether.IntegrationTests
{
    [TestClass]
    public class QualityCheckFlowTests
    {
        internal WebApplicationFactory<Startup> _factory;
        internal Mock<IYeOldDependencyTest> _mockDependency;

        public QualityCheckFlowTests()
        {
            _factory = new WebApplicationFactory<Startup>();
            _mockDependency = new Mock<IYeOldDependencyTest>();
        }

        [TestMethod]
        public async Task QualityCheckFlow_VerifyQualityCheckDependencies_Returns500()
        {
            var server = _factory.Server;

            using var client = server.CreateClient();
            var result = await client.GetAsync("/api/QualityCheck");

            Assert.IsTrue(!result.IsSuccessStatusCode);
        }

        [TestMethod]
        public async Task QualityCheckFlow_RunAndTearDown_BothCallsInvoked()
        {
            InjectDummyDependencies();

            var server = _factory.Server;

            using var client = server.CreateClient();
            var result = await client.GetAsync("/api/QualityCheck");

            _mockDependency.Verify(m => m.FindGoldAsync(), Times.AtLeastOnce);
            _mockDependency.Verify(m => m.DeleteGoldAsync(), Times.AtLeastOnce);

        }

        private void InjectDummyDependencies()
        {
            WebApplicationFactory<Startup> internalFactory = new WebApplicationFactory<Startup>();

            _factory = internalFactory.WithWebHostBuilder(builder =>
            {
                builder.ConfigureTestServices(s => s.AddSingleton(_mockDependency.Object));
            });
        }
    }
}
