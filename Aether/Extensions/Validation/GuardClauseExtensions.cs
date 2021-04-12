using System;
using System.Collections.Generic;
using System.Net.Http;

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
    }
}
