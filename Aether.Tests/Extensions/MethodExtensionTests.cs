using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Aether.Extensions;
using System.Text;
using System.Reflection;
using static Aether.Extensions.MethodExtensions;
using Aether.CustomAttributes;

namespace Aether.Tests.Extensions
{
    [TestClass]
    public class MethodExtensionTests
    {
        [IgnoreCleanup]
        private void TestFunction()
        {
        }

        private void TestFunctionNoAttribute()
        {
        }

        [TestMethod]
        public void HasAttribute_True()
        {
            var hasAttribute = HasAttribute<IgnoreCleanupAttribute>(() => TestFunction());

            Assert.IsTrue(hasAttribute);
        }

        [TestMethod]
        public void HasAttribute_False()
        {
            var hasAttribute = HasAttribute<IgnoreCleanupAttribute>(() => TestFunctionNoAttribute());

            Assert.IsFalse(hasAttribute);
        }
    }
}
