using Aether.QualityChecks.Extensions;
using Microsoft.AspNetCore.Builder;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;

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