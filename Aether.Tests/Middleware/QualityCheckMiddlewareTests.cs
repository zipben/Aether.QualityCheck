using Aether.QualityChecks.Helpers;
using Aether.QualityChecks.Interfaces;
using Aether.QualityChecks.Middleware;
using Aether.QualityChecks.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Aether.Middleware.Tests
{
    [TestClass()]
    public class QualityCheckMiddlewareTests
    {

        private Mock<RequestDelegate> _mockNext;
        private Mock<HttpContext> _mockHttpContext;
        private Mock<IQualityCheckExecutionHandler> _mockHandler;
        private List<IQualityCheck> _mockQualityChecks;
        private QualityCheckMiddleware _target;
        private Mock<HttpRequest> _mockRequest;

        [TestInitialize]
        public void Init()
        {
            _mockNext = new Mock<RequestDelegate>();
            _mockHandler = new Mock<IQualityCheckExecutionHandler>();
            _mockHttpContext = new Mock<HttpContext>();
            _mockQualityChecks = new List<IQualityCheck>();
            _target = new QualityCheckMiddleware(_mockNext.Object, _mockQualityChecks, _mockHandler.Object, "/testendpoint");
            Setup_Mocks();
        }

        public void Setup_Mocks(bool shouldThrowException = false, bool requestStarted = false)
        {
            if (shouldThrowException)
            {
                _mockNext.Setup(x => x(It.IsAny<HttpContext>()))
                         .Throws<Exception>();
            }

            _mockHttpContext.Setup(x => x.Response.HasStarted)
                            .Returns(requestStarted);

            _mockRequest = new Mock<HttpRequest>();
            _mockRequest.SetupAllProperties();
            _mockRequest.Setup(x => x.Path)
                        .Returns(new PathString("/testendpoint"));

            _mockHttpContext.Setup(x => x.Request)
                            .Returns(_mockRequest.Object);

            _mockHttpContext.Setup(x => x.Response.Body.WriteAsync(It.IsAny<ReadOnlyMemory<byte>>(), It.IsAny<CancellationToken>()))
                            .Returns(new ValueTask());

            
        }

        [TestMethod()]
        [ExpectedException(typeof(ArgumentNullException))]
        public void QualityCheckMiddlewareTest_AllDependenciesNull_ThrowsArgumentNullException()
        {
            QualityCheckMiddleware target = new QualityCheckMiddleware(null, null, null, null, null);
        }

        [TestMethod()]
        [ExpectedException(typeof(ArgumentNullException))]
        public async Task QualityCheckMiddlewareTest_Invoke_ThrowsArgumentNullException()
        {
            await _target.Invoke(null);
            
        }

        [TestMethod()]
        public async Task QualityCheckMiddlewareTest_InvokeQualityEndpoint()
        {
            await _target.Invoke(_mockHttpContext.Object);

        }

        [TestMethod()]
        public async Task QualityCheckMiddlewareTest_InvokeIncorrectQualityEndpoint()
        {
            _mockRequest.Setup(x => x.Path)
                        .Returns(new PathString("/wrongEndpoint"));
            await _target.Invoke(_mockHttpContext.Object);

        }

        [TestMethod()]
        public async Task QualityCheckMiddlewareTest_InvokeHasTests()
        {
            QualityCheckSetup(false);
            
            await _target.Invoke(_mockHttpContext.Object);

        }

        [TestMethod()]
        public async Task QualityCheckMiddlewareTest_InvokeHasTestsButTheyFail()
        {
            QualityCheckSetup(true);
            
            await _target.Invoke(_mockHttpContext.Object);

        }

        public void QualityCheckSetup(bool input)
        {
            var qualityCheck = new Mock<IQualityCheck>();
            qualityCheck.Setup(x => x.LogName)
                        .Returns("MockQualityCheck");
            _mockHandler.Setup(x => x.ExecuteQualityCheck(It.IsAny<IQualityCheck>()))
                        .ReturnsAsync(new QualityCheckResponseModel("TestStep") { Steps = new List<StepResponse>() { new StepResponse() { StepPassed = input } } });
            _mockQualityChecks.Add(qualityCheck.Object);
        }
    }
}