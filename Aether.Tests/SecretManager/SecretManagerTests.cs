using Microsoft.VisualStudio.TestTools.UnitTesting;
using Aether.SecretManager;
using System;
using System.Collections.Generic;
using System.Text;

namespace Aether.SecretManager.Tests
{
    [TestClass()]
    public class SecretManagerTests
    {
        [TestMethod()]
        public void PopulateSecretsToEnvironmentTest_WithEnvironmentFilters_EnvironmentShouldHaveVariables()
        {

            Dictionary<string, string> tagFilters = new Dictionary<string, string>()
            {
                {"environment", "test"}
            };

            SecretManager.PopulateSecretsToEnvironment("us-east-2", tagFilters);

            Assert.IsTrue(Environment.GetEnvironmentVariables().Count > 0);
        }
    }
}