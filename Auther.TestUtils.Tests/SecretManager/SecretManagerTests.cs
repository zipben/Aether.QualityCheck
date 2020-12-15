using Microsoft.VisualStudio.TestTools.UnitTesting;
using Aether.TestUtils.Helpers;
using System;
using System.Collections.Generic;
using System.Text;

namespace Aether.TestUtiles.Tests.SecretManager
{
    [TestClass]
    public class SecretManagerTests
    {
        [TestMethod]
        public void PopulateSecretsToEnvironmentTest_WithEnvironmentFilters_EnvironmentShouldHaveVariables()
        {
            Dictionary<string, string> tagFilters = new Dictionary<string, string>()
            {
                {"environment", "test"}
            };

            TestUtils.Helpers.SecretManager.PopulateSecretsToEnvironment("us-east-2", tagFilters);

            Assert.IsTrue(Environment.GetEnvironmentVariables().Count > 0);
        }
    }
}