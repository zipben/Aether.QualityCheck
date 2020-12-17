using System;
using System.Collections.Generic;
using Aether.Extensions;
using Amazon.SecretsManager;
using Amazon.SecretsManager.Model;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Auther.Tests.Extensions
{
    [TestClass]
    public class SecretManagerHelperExtensionsTests
    {
        private string _testKey;
        private List<string> _testValueList;
        private FilterNameStringType _testFilterNameStringType;

        [TestInitialize]
        public void Init()
        {
            _testKey = Guid.NewGuid().ToString();
            _testValueList = new List<string> { Guid.NewGuid().ToString() };
            _testFilterNameStringType = new FilterNameStringType(_testKey);
        }

        [TestMethod]
        public void SecretManagerHelperExtensionsTest()
        {
            var listRequest = new ListSecretsRequest();
            listRequest.AddFilter(_testFilterNameStringType, _testValueList);

            VerifyTestKeyAndValuesAreInFilter(listRequest);
        }

        private void VerifyTestKeyAndValuesAreInFilter(ListSecretsRequest listRequest)
        {
            var containsFilter = false;
            foreach (var filter in listRequest.Filters)
            {
                if (filter.Key.Value == _testKey)
                {
                    containsFilter = true;
                    foreach (var value in filter.Values)
                    {
                        Assert.IsTrue(_testValueList.Contains(value));
                    }
                }
            }
            Assert.IsTrue(containsFilter);
        }
    }
}
