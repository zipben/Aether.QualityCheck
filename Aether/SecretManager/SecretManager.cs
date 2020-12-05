using Aether.Extensions;
using Amazon;
using Amazon.SecretsManager;
using Amazon.SecretsManager.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aether.SecretManager
{
    public class SecretManager
    {
        /// <summary>
        /// Inserts all the secrets from secret manager that match the tags and region provided
        /// </summary>
        /// <param name="region">The region you want to retrieve secrets from</param>
        /// <param name="tagFilters">key:Tag Key, Value:The tag value you would like to filter down to</param>
        public static void PopulateSecretsToEnvironment(string region, Dictionary<string, string> tagFilters)
        {
            using IAmazonSecretsManager client = new AmazonSecretsManagerClient(RegionEndpoint.GetBySystemName(region));

            List<SecretListEntry> secrets = GetAllSecretListEntriesFromRemote(client, tagFilters);
            
            List<GetSecretValueResponse> secretValues = GetSecretValues(client, secrets);

            CopyToEnvironment(secretValues);
        }

        private static void CopyToEnvironment(List<GetSecretValueResponse> secretValues)
        {
            foreach (var secret in secretValues)
            {
                string key = secret.Name.Split('/').Last();
                string value = secret.SecretString;

                Environment.SetEnvironmentVariable(key, value);
            }
        }

        private static List<GetSecretValueResponse> GetSecretValues(IAmazonSecretsManager client, List<SecretListEntry> themisSecrets)
        {
            List<GetSecretValueResponse> ret = new List<GetSecretValueResponse>();

            foreach (var secret in themisSecrets)
            {
                GetSecretValueRequest request = new GetSecretValueRequest();
                request.SecretId = secret.Name;

                var response = client.GetSecretValueAsync(request).Result;

                ret.Add(response);
            }

            return ret;
        }

        private static List<SecretListEntry> GetAllSecretListEntriesFromRemote(IAmazonSecretsManager client, Dictionary<string,string> tagFilters, string nextToken = null)
        {
            ListSecretsRequest listRequest = new ListSecretsRequest();
            listRequest.MaxResults = 100;
            listRequest.NextToken = nextToken;

            if(tagFilters.IsNotEmpty())
            {
                listRequest = AddFiltersToRequest(listRequest, tagFilters);
            }

            var response = client.ListSecretsAsync(listRequest).Result;

            if (response.NextToken != null)
            {
                response.SecretList.AddRange(GetAllSecretListEntriesFromRemote(client, tagFilters, response.NextToken));
            }

            return response.SecretList;
        }

        private static ListSecretsRequest AddFiltersToRequest(ListSecretsRequest listRequest, Dictionary<string, string> tagFilters)
        {
            var filters = new List<Filter>();

            //You can thank AWS for this super awesome readable query structure.  I know what you are thinking, "yo, I probably want to filter by both the tag type, and the value"
            //Thats totally reasonable, but buckle up, cause you need to add both as seperate filters.  I hope you were hankering for the cartesian product of your filters. 
            foreach(var kvp in tagFilters)
            {
                listRequest.AddFilter(FilterNameStringType.TagKey, kvp.Key.CreateList());
                listRequest.AddFilter(FilterNameStringType.TagValue, kvp.Value.CreateList());
            }

            listRequest.Filters.AddRange(filters);

            return listRequest;
        }
    }
}
