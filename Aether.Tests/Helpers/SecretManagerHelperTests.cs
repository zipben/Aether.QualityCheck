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

            Aether.Helpers.SecretManagerHelper.PopulateAllAppSecrets("207953", "test", "us-east-2");

            Assert.IsTrue(Environment.GetEnvironmentVariables().Count > 0);
        }
    }
}