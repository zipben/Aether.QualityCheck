using System;

namespace Aether.Extensions
{
    public static class LongDateExtensions
    {
        public static DateTime SecondsToDate(this long val) =>
            DateTimeOffset.FromUnixTimeSeconds(val).DateTime;

        public static DateTime? SecondsToDate(this long? val) =>
            val.HasValue 
                ? val.Value.SecondsToDate()
                : (DateTime?) null;

        public static DateTime MillisecondsToDate(this long val) =>
            DateTimeOffset.FromUnixTimeMilliseconds(val).DateTime;

        public static DateTime? MillisecondsToDate(this long? val) =>
            val.HasValue
                ? val.Value.MillisecondsToDate()
                : (DateTime?) null;
    }
}
