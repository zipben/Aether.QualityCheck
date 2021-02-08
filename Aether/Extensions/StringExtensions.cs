using System;
using System.Text.RegularExpressions;

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

        /// <summary>
        /// Splits a Camel case string on Capitals.  Should ignore uppercase abbreviates like CSV, USA etc. 
        /// </summary>
        /// <param name="val"></param>
        /// <param name="delimeter"></param>
        /// <returns></returns>
        public static string SplitCamelCase(this string val, string delimeter = " ")
        {
            var r = new Regex(@"
                (?<=[A-Z])(?=[A-Z][a-z]) |
                 (?<=[^A-Z])(?=[A-Z]) |
                 (?<=[A-Za-z])(?=[^A-Za-z])", RegexOptions.IgnorePatternWhitespace);

            return r.Replace(val, delimeter);
        }
    }
}
