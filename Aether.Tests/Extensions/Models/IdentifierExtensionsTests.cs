using System.Collections.Generic;
using System.Linq;
using Aether.Enums;
using Aether.Extensions;
using Aether.Extensions.Models;
using Aether.Interfaces;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Aether.Tests.Extensions.Models
{
    [TestClass]
    public class IdentifierExtensionsTests
    {
        private class TestIdentifier : IIdentifier
        {
            public IdentifierType IdentifierType { get; set; }
            public List<string> IdentifierValues { get; set; }
        }

        private TestIdentifier _testIdentifier;
        private List<TestIdentifier> _testIdentifierList;

        [TestInitialize]
        public void Init()
        {
            _testIdentifier = new TestIdentifier { IdentifierType = IdentifierType.GCID, IdentifierValues = new List<string> { "12345" } };
            _testIdentifierList = _testIdentifier.CreateList();
        }


        [TestMethod]
        public void ToKafkaIdentifiersTest()
        {
            var result = _testIdentifierList.ToKafkaIdentifiers().ToList();

            Assert.AreEqual(_testIdentifierList.Count, result.Count);
        }

        [TestMethod]
        public void ToKafkaIdentifierTest()
        {
            var result = _testIdentifier.ToKafkaIdentifier();

            Assert.AreEqual(_testIdentifier.IdentifierValues.Count, result.Value.Count);
            Assert.AreEqual(_testIdentifier.IdentifierValues[0], result.Value[0]);
        }
    }
}
