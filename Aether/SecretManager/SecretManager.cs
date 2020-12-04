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
        public static void PopulateSecretsToEnvironment(string region, Dictionary<string, string> tagFilters)
        {
            
            IAmazonSecretsManager client = new AmazonSecretsManagerClient(RegionEndpoint.GetBySystemName(region));

            List<SecretListEntry> secrets;

            secrets = GetAllSecretListEntriesFromRemote(client, tagFilters);
            
            List<GetSecretValueResponse> secretValues = GetSecretValues(client, secrets);

            CopyToEnvironment(secretValues);
        }

        private static List<SecretListEntry> GenerateDummySecretListEntries(List<string> secretNames)
        {
            return secretNames.Select(n => new SecretListEntry() { Name = n }).ToList();
        }

        private static List<SecretListEntry> FilterSecretsByTag(string tagKey, string tagValue, List<SecretListEntry> allSecrets)
        {
            return allSecrets.FindAll(s => s.Tags.Any(t => t.Key.Like(tagKey) && t.Value.Like(tagValue)));
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

                var response = client.GetSecretValueAsync(request);

                Task.WaitAll(response);

                ret.Add(response.Result);
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

            var response = client.ListSecretsAsync(listRequest);

            Task.WaitAll(response);

            if (response.Result.NextToken != null)
            {
                response.Result.SecretList.AddRange(GetAllSecretListEntriesFromRemote(client, tagFilters, response.Result.NextToken));
            }

            return response.Result.SecretList;
        }

        private static ListSecretsRequest AddFiltersToRequest(ListSecretsRequest listRequest, Dictionary<string, string> tagFilters)
        {
            var filters = new List<Filter>();

            //You can thank AWS for this super awesome readable query structure.  I know what you are thinking, "yo, I probably want to filter by both the tag type, and the value"
            //Thats totally reasonable, but buckle up, cause you need to add both as seperate filters.  I hope you were hankering for the cartesian product of your filters. 
            foreach(var kvp in tagFilters)
            {
                var newTagKeyFilter = new Filter()
                {
                    Key = FilterNameStringType.TagKey,
                    Values = kvp.Key.CreateList()
                };

                var newTagValueFilter = new Filter()
                {
                    Key = FilterNameStringType.TagValue,
                    Values = kvp.Value.CreateList()
                };

                filters.Add(newTagKeyFilter);
                filters.Add(newTagValueFilter);
            }

            listRequest.Filters.AddRange(filters);

            return listRequest;
        }
    }
}
