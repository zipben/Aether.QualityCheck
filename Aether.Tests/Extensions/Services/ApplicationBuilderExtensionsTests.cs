using Microsoft.VisualStudio.TestTools.UnitTesting;
using Aether.Extensions;
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Builder;
using Moq;
using Microsoft.AspNetCore.Http;
using Aether.Middleware;

namespace Aether.Extensions.Tests
{
    [TestClass()]
    public class ApplicationBuilderExtensionsTests
    {
        private Mock<IApplicationBuilder> _applicationBuilder;

        [TestInitialize]
        public void Init()
        {
            _applicationBuilder = new Mock<IApplicationBuilder>();

        } 

        [TestMethod()]
        public void UseQualityCheckMiddlewareTest()
        {
            var x = _applicationBuilder.Object.UseQualityCheckMiddleware();
            Assert.IsNull(x);
        }

        [TestMethod()]
        [ExpectedException(typeof(ArgumentException))]
        public void UseQualityCheckMiddlewareIllegalRouteTest()
        {
            var x = _applicationBuilder.Object.UseQualityCheckMiddleware(" ");
        }
    }
}