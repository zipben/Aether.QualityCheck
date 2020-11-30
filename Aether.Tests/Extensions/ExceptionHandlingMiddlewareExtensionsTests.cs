using System;
using Aether.Extensions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace Aether.Tests.Extensions
{
    [TestClass]
    public class ExceptionHandlingMiddlewareExtensionsTests
    {
        [TestMethod]
        public void UseExceptionHandlingMiddlewareTest()
        {
            Mock<IApplicationBuilder> builder = new Mock<IApplicationBuilder>();

            builder.Object.UseExceptionHandlingMiddleware();

            builder.Verify(x => x.Use(It.IsAny<Func<RequestDelegate, RequestDelegate>>()), Times.Once);
        }
    }
}
