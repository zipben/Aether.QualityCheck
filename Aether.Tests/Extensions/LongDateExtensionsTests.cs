using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Aether.Extensions;

namespace Aether.Tests.Extensions
{
    [TestClass]
    public class LongDateExtensionsTests
    {
        private static IEnumerable<DateTime?> TestData(bool includeMilliseconds)
        {
            yield return new DateTime(1863, 7, 27, 18, 34, 50, includeMilliseconds ? 68 : 0, DateTimeKind.Utc);
            yield return new DateTime(1970, 2, 15, 0, 0, 0, includeMilliseconds ? 68 : 0, DateTimeKind.Utc);
            yield return new DateTime(1993, 2, 15, 6, 2, 18, includeMilliseconds ? 12 : 0, DateTimeKind.Utc);
            yield return new DateTime(2031, 12, 30, 22, 59, 59, includeMilliseconds ? 12 : 0, DateTimeKind.Utc);
            yield return DateTime.UtcNow.Date;
            yield return null;
        }

        private static IEnumerable<object[]> SecondsToDateTestData()
        {
            foreach (var date in TestData(false))
            {
                yield return new object[] { date.HasValue ? new DateTimeOffset(date.Value).ToUnixTimeSeconds() : (long?) null, date };
            }
        }
        private static IEnumerable<object[]> MillisecondsToDateTestData()
        {
            foreach (var date in TestData(true))
            {
                yield return new object[] { date.HasValue ? new DateTimeOffset(date.Value).ToUnixTimeMilliseconds() : (long?) null, date };
            }
        }

        [TestMethod]
        [DynamicData(nameof(SecondsToDateTestData), DynamicDataSourceType.Method)]
        public void SecondsToDateTest(long? longDate, DateTime? expectedDate)
        {
            Assert.AreEqual(expectedDate, longDate.SecondsToDate());
        }

        [TestMethod]
        [DynamicData(nameof(MillisecondsToDateTestData), DynamicDataSourceType.Method)]
        public void MillisecondsToDateTest(long? longDate, DateTime? expectedDate)
        {
            Assert.AreEqual(expectedDate, longDate.MillisecondsToDate());
        }
    }
}
