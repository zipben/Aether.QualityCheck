using System;
using System.Collections.Generic;
using System.Net.Http;
using Microsoft.Extensions.Configuration;

namespace Ardalis.GuardClauses
{
    public static class GuardClauseExtensions
    {
        /// <summary>
        /// Throws an ArgumentNullException if the key is not present in the dictionary
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="TVal"></typeparam>
        /// <param name="guardClause"></param>
        /// <param name="input"></param>
        /// <param name="key"></param>
        /// <param name="parameterName"></param>
        public static void MissingKey<T, TVal>(this IGuardClause guardClause, Dictionary<T, TVal> input, T key, string parameterName)
        {
            if (!input.ContainsKey(key))
                throw new ArgumentNullException($"{parameterName} must be present in dictionary");
        }

        /// <summary>
        /// Throws an HttpRequestException if the Http Response code insidated the call was unsuccessful
        /// </summary>
        /// <param name="guardClause"></param>
        /// <param name="httpResponse"></param>
        public static void UnsuccessfulHttpRequest(this IGuardClause guardClause, HttpResponseMessage httpResponse)
        {
            if (!httpResponse.IsSuccessStatusCode)
                throw new HttpRequestException($"{httpResponse.RequestMessage.Method} call to {httpResponse.RequestMessage.RequestUri} returned {httpResponse.StatusCode}: {httpResponse.ReasonPhrase}");
        }

        public static IConfigurationSection MissingConfigurationSection(this IGuardClause guardClause, IConfiguration configuration, string sectionName)
        {
            var section = configuration.GetSection(sectionName);

            if (!section.Exists())
                throw new ArgumentNullException($"Configuration section {sectionName} must be defined");
            else
                return section;
        }

        public static string MissingConfigurationValue(this IGuardClause guardClause, IConfigurationSection section)
        {
            if (string.IsNullOrWhiteSpace(section.Value))
                throw new ArgumentException($"Configuration {section.Path} must be set");
            else
                return section.Value;
        }

        public static string MissingConfigurationValue(this IGuardClause guardClause, IConfiguration configuration, string keyName)
        {
            var value = configuration.GetValue<string>(keyName);

            if (string.IsNullOrWhiteSpace(value))
                throw new ArgumentException($"Configuration {keyName} must be set");
            else
                return value;
        }
    }
}
