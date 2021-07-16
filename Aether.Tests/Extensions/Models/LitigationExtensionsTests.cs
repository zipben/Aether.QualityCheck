using System;
using System.Collections.Generic;
using Aether.Enums;
using Aether.Extensions.Models;
using Aether.Interfaces;
using Aether.Interfaces.Themis;
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

        private class TestLitigation : ILitigation
        {
            public string Id { get ; set ; }
            public string CaseName { get; set; }
            public DateTime DateHoldCreated { get; set; }
            public DateTime? DateHoldEnded { get; set; }
            public List<IIdentifier> InputIdentifiers { get; set; }
            public List<IIdentifier> ResolvedIdentifiers { get; set; }
            public DateTime LastUpdateDate { get; set; }
            public DateTime CreateDate { get; set; }
        }

        public static IEnumerable<object[]> HasInputIdentifiersTestData()
        {
            var litigationWithInputidentifiers = new TestLitigation { CaseName = "abc", DateHoldCreated = DateTime.Now, InputIdentifiers = new List<IIdentifier> { new TestIdentifier { IdentifierType = IdentifierType.GCID, IdentifierValues = new List<string> { "1235" } } } };
            var litigationWithNullIdentifiers = new TestLitigation { CaseName = "abc", DateHoldCreated = DateTime.Now, InputIdentifiers = null };
            var litigationWithZeroIdentifierCount = new TestLitigation { CaseName = "abc", DateHoldCreated = DateTime.Now, InputIdentifiers = new List<IIdentifier> { } };

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
