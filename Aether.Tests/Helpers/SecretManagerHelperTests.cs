using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Aether.Tests.Helpers
{
    [TestClass]
    public class SecretManagerHelperTests
    {
        [TestMethod]
        public void PopulateSecretsToEnvironmentTest()
        {
            var tagFilters = new Dictionary<string, string>()
            {
                {"environment", "test"}
            };

            TestUtils.Helpers.SecretManagerHelper.PopulateSecretsToEnvironment("us-east-2", tagFilters);

            Assert.IsTrue(Environment.GetEnvironmentVariables().Count > 0);
        }
    }
}