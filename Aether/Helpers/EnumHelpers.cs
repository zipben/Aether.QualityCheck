using System;
using System.Text.RegularExpressions;

namespace Aether.Helpers
{
    public static class EnumHelpers
    {
        public static string GetFriendlyName<T>(T value) where T : struct, IConvertible
        {
            if (!typeof(T).IsEnum)
            {
                throw new ArgumentException("T must be an enumerated type");
            }

            string str = Enum.GetName(typeof(T), value);

            return Regex.Replace(
                Regex.Replace(
                    str,
                    @"(\P{Ll})(\P{Ll}\p{Ll})",
                    "$1 $2"
                ),
                @"(\p{Ll})(\P{Ll})",
                "$1 $2"
            );

        }
    }
}
