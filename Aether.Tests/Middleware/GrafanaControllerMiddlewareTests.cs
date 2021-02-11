using Aether.Middleware;
using APILogger.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using RockLib.Metrics;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Aether.Tests.Middleware
{
    [TestClass]
    public class GrafanaControllerMiddlewareTests
    {
        private GrafanaControllersMiddleware _target;

        private Mock<RequestDelegate> _mockNext;
        private Mock<IApiLogger> _mockLogger;
        private Mock<IMetricFactory> _mockMetricFactory;
        private Mock<HttpContext> _mockHttpContext;

        private string _testString = Guid.NewGuid().ToString();

        [TestInitialize]
        public void Init()
        {
            _mockNext = new Mock<RequestDelegate>();
            _mockLogger = new Mock<IApiLogger>();
            _mockHttpContext = new Mock<HttpContext>();
            _mockMetricFactory = new Mock<IMetricFactory>();

            _target = new GrafanaControllersMiddleware(_mockLogger.Object, _mockNext.Object, _mockMetricFactory.Object);

            SetupMocks();
        }

        private void SetupMocks(bool shouldThrowException = false, bool requestStarted = false)
        {
            if (shouldThrowException)
            {
                _mockNext.Setup(x => x(It.IsAny<HttpContext>()))
                    .Throws<Exception>();

                _mockMetricFactory.Setup(x => x.CreateWhitebox(It.IsAny<Operation>()))
                  .Returns(new Metric(new ConsoleMetricsClient(), MetricType.Whitebox, MetricCategory.Http, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty));
            }

            _mockHttpContext.Setup(x => x.Response.HasStarted)
                .Returns(requestStarted);

            _mockHttpContext.Setup(hc => hc.Request).Returns(new Mock<HttpRequest>().Object);

            _mockHttpContext.Setup(x => x.Response.Body.WriteAsync(It.IsAny<ReadOnlyMemory<byte>>(), It.IsAny<CancellationToken>()))
                .Returns(new ValueTask());
        }

        [TestMethod]
        public void ArgumentNullExceptionTest()
        {
            Assert.ThrowsException<ArgumentNullException>(() => _target = new GrafanaControllersMiddleware(null, null, null));
        }

        [TestMethod]
        public async Task InvokeTest()
        {
            await _target.Invoke(_mockHttpContext.Object);

            _mockNext.Verify(x => x(_mockHttpContext.Object), Times.Once);
            _mockMetricFactory.Verify(mf => mf.CreateWhitebox(It.IsAny<Operation>()), Times.Once);

        }

        [TestMethod]
        public async Task InvokeTest_FilterFound_CallsMetricZeroTimes()
        {
            var request = new Mock<HttpRequest>();
            request.Setup(r => r.Path).Returns(() => "/api/heartbeat");
            _mockHttpContext.Setup(hc => hc.Request).Returns(request.Object);
            
            await _target.Invoke(_mockHttpContext.Object);

            _mockNext.Verify(x => x(_mockHttpContext.Object), Times.Once);
            _mockMetricFactory.Verify(mf => mf.CreateWhitebox(It.IsAny<Operation>()), Times.Never);
        }

        [TestMethod]
        public async Task InvokeTest_FilterFound_CallsQueryCondition()
        {
            var request = new Mock<HttpRequest>();

            var queryString = QueryString.Create("person", "John");

            request.Setup(r => r.Path).Returns(() => "/api/something");
            request.Setup(r => r.QueryString).Returns(queryString);
            _mockHttpContext.Setup(hc => hc.Request).Returns(request.Object);

            await _target.Invoke(_mockHttpContext.Object);

            _mockNext.Verify(x => x(_mockHttpContext.Object), Times.Once);
            _mockMetricFactory.Verify(mmf => mmf.CreateWhitebox(It.Is<Operation>(o => o.Name.Contains("John"))));
        }
    }
}
