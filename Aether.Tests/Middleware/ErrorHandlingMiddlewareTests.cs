﻿using System;
using System.Threading;
using System.Threading.Tasks;
using Aether.Middleware;
using APILogger.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace Aether.Tests.Middleware
{
    [TestClass]
    public class ErrorHandlingMiddlewareTests
    {
        private ErrorHandlingMiddleware _target;

        private Mock<RequestDelegate> _mockNext;
        private Mock<IApiLogger> _mockLogger;
        private Mock<HttpContext> _mockHttpContext;

        private string _testString = Guid.NewGuid().ToString();

        [TestInitialize]
        public void Init()
        {
            _mockNext = new Mock<RequestDelegate>();
            _mockLogger = new Mock<IApiLogger>();
            _mockHttpContext = new Mock<HttpContext>();

            _target = new ErrorHandlingMiddleware(_mockLogger.Object, _mockNext.Object);

            SetupMocks();
        }

        private void SetupMocks(bool shouldThrowException = false, bool requestStarted = false)
        {
            if (shouldThrowException)
            {
                _mockNext.Setup(x => x(It.IsAny<HttpContext>()))
                    .Throws<Exception>();
            }

            _mockHttpContext.Setup(x => x.Response.HasStarted)
                .Returns(requestStarted);

            _mockHttpContext.Setup(x => x.Response.Body.WriteAsync(It.IsAny<ReadOnlyMemory<byte>>(), It.IsAny<CancellationToken>()))
                .Returns(new ValueTask());
        }

        [TestMethod]
        public void ArgumentNullExceptionTest()
        {
            Assert.ThrowsException< ArgumentNullException>(() => _target = new ErrorHandlingMiddleware(null, null));
        }

        [TestMethod]
        public async Task InvokeTest()
        {
            await _target.Invoke(_mockHttpContext.Object);

            _mockNext.Verify(x => x(_mockHttpContext.Object), Times.Once);
        }

        [TestMethod]
        [DataRow(true, true, true)]
        [DataRow(true, false, true)]
        [DataRow(false, true, false)]
        [DataRow(false, false, false)]
        public async Task InvokeTest_Exception(bool requestStarted, bool prod, bool exceptionExpected)
        {
            if (prod)
            {
                Environment.SetEnvironmentVariable("ASPNETCORE_ENVIRONMENT", "Production");
            }

            SetupMocks(true, requestStarted);

            if (exceptionExpected)
            {
                await Assert.ThrowsExceptionAsync<Exception>(() => _target.Invoke(_mockHttpContext.Object));
                _mockLogger.Verify(x => x.LogWarning(It.IsAny<string>(), null, It.IsAny<Exception>()), Times.Once);
            }
            else
            {
                await _target.Invoke(_mockHttpContext.Object);
                _mockLogger.Verify(x => x.LogError(It.IsAny<string>(), null, It.IsAny<Exception>()), Times.Once);
            }
        }
    }
}
