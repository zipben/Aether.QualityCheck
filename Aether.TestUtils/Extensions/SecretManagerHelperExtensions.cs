using Amazon.SecretsManager;
using Amazon.SecretsManager.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace Aether.TestUtils.Extensions
{
    internal static class SecretManagerHelperExtensions
    {
        public static void AddFilter(this ListSecretsRequest listRequest, FilterNameStringType key, List<string> values) =>
            listRequest.Filters.Add(new Filter { Key = key, Values = values });
    }
}
