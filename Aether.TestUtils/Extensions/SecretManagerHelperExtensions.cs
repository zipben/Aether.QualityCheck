using System.Collections.Generic;
using Amazon.SecretsManager;
using Amazon.SecretsManager.Model;

namespace Aether.TestUtils.Extensions
{
    public static class SecretManagerHelperExtensions
    {
        public static void AddFilter(this ListSecretsRequest listRequest, FilterNameStringType key, List<string> values) =>
            listRequest.Filters.Add(new Filter { Key = key, Values = values });
    }
}
