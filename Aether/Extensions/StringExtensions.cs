using System;

namespace Aether.Extensions
{
    public static class StringExtensions
    {
        /// <summary>
        /// Case insentitive equals
        /// </summary>
        /// <param name="str"></param>
        /// <param name="comparisonValue"></param>
        /// <returns></returns>
        public static bool Like(this string str, string comparisonValue)
        {
            return str.Equals(comparisonValue, StringComparison.CurrentCultureIgnoreCase);
        }

        /// <summary>
        /// String is not null nor whitespace
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static bool Exists(this string str) =>
            !string.IsNullOrWhiteSpace(str);
    }
}
