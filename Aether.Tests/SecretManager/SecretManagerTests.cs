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
        public void PopulateSecretsToEnvironmentTest_GetAllSecrets()
        {
            SecretManager.PopulateSecretsToEnvironment("us-east-2", null);
        }

        [TestMethod()]
        public void PopulateSecretsToEnvironmentTest_WithFilters()
        {

            Dictionary<string, string> tagFilters = new Dictionary<string, string>()
            {
                {"app-id", "206980"},
                {"environment", "test"}
            };

            SecretManager.PopulateSecretsToEnvironment("us-east-2", tagFilters);
        }
    }
}