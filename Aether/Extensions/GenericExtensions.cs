﻿using System;
using System.Net.Http;
using System.Net.Http.Headers;
using Newtonsoft.Json;

namespace Aether.Extensions
{
    public static class GenericExtensions
    {
        /// <summary>
        /// Returns a new cloned instance of a simple object.  Please dont use for massive objects.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="parent"></param>
        /// <returns></returns>
        public static T SluggishClone<T>(this T parent)
        {
            string json = JsonConvert.SerializeObject(parent);

            var clone = JsonConvert.DeserializeObject<T>(json);

            return clone;
        }

        /// <summary>
        /// Generates a hash for a given object to allow for quick object comparisons.  Should not be used in places where performance is critical
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="hashTarget"></param>
        /// <returns></returns>
        public static int SluggishHash<T>(this T hashTarget)
        {
            string json = JsonConvert.SerializeObject(hashTarget);

            return json.GetHashCode();
        }

        /// <summary>
        /// Serializes the input model into Http String Content
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="model"></param>
        /// <returns></returns>
        public static HttpContent GenerateHttpStringContent<T>(this T model)
        {
            string jsonString;
            try
            {
                jsonString = JsonConvert.SerializeObject(model);
            }
            catch (Exception e)
            {
                throw new JsonSerializationException($"{nameof(Aether)}.{nameof(GenerateHttpStringContent)} encountered an exception while trying to serialize request model into string content", e);
            }

            var content = new StringContent(jsonString);
            content.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            return content;
        }

    }
}