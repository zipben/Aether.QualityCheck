using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;

namespace Aether.Extensions
{
    public static class ObjectExtensions
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
        public static int SluggishHash(this object hashTarget)
        {
            string json = JsonConvert.SerializeObject(hashTarget);

            return json.GetHashCode();
        }
    }
}
