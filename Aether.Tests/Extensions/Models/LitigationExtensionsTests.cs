using System;
using System.Collections.Generic;
using Aether.Enums;
using Aether.Extensions.Models;
using Aether.Interfaces;
using Aether.Interfaces.Themis;
using Aether.Models.Themis;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Aether.Tests.Extensions
{
    [TestClass]
    public class LitigationExtensionsTests
    {
        private class TestIdentifier : IIdentifier
        {
            public IdentifierType IdentifierType { get; set; }
            public List<string> IdentifierValues { get; set; }
        }

        public static IEnumerable<object[]> HasInputIdentifiersTestData()
        {
            var litigationWithInputidentifiers = new Litigation { CaseName = "abc", DateHoldCreated = DateTime.Now, InputIdentifiers = new List<IIdentifier> { new TestIdentifier { IdentifierType = IdentifierType.GCID, IdentifierValues = new List<string> { "1235" } } } };
            var litigationWithNullIdentifiers = new Litigation { CaseName = "abc", DateHoldCreated = DateTime.Now, InputIdentifiers = null };
            var litigationWithZeroIdentifierCount = new Litigation { CaseName = "abc", DateHoldCreated = DateTime.Now, InputIdentifiers = new List<IIdentifier> { } };

            yield return new object[] { litigationWithInputidentifiers, true };
            yield return new object[] { litigationWithNullIdentifiers, false };
            yield return new object[] { litigationWithZeroIdentifierCount, false };
        }

        [TestMethod]
        [DynamicData(nameof(HasInputIdentifiersTestData), DynamicDataSourceType.Method)]
        public void HasInputIdentifiersTest(ILitigation testlitigation, bool expectedResult)
        {
            var actualResult = testlitigation.HasInputIdentifiers();
            Assert.AreEqual(expectedResult, actualResult);
        }
    }
}
