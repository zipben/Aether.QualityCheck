using System;
using System.Collections.Generic;
using System.Linq;
using Aether.Extensions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Aether.Tests.Extensions
{
    [TestClass]
    public class CollectionExtensionsTests
    {
        [TestMethod]
        public void BatchTest_Ints()
        {
            List<int> testInts = new List<int>()
            {
                1,2,3,4,5,6,7,8,9
            };

            var result = testInts.Batch(3);

            Assert.AreEqual(3, result.Count);
        }

        [TestMethod]
        public void BatchTest_Ints_ZeroBatch()
        {
            List<int> testInts = new List<int>()
            {
                1,2,3,4,5,6,7,8,9
            };

            var result = testInts.Batch(0);

            Assert.AreEqual(1, result.Count);
        }

        [TestMethod]
        public void BatchTest_Ints_NullList()
        {
            List<int> testInts = null;

            Assert.ThrowsException<ArgumentNullException>(() => testInts.Batch(0));
        }

        [TestMethod]
        public void CreateListTest()
        {
            var testString = Guid.NewGuid().ToString();
            var result = testString.CreateList();

            Assert.AreEqual(1, result.Count);
            Assert.AreEqual(testString, result.Single());
        }

        [TestMethod]
        public void AddIfNotNullTest()
        {
            var testString = Guid.NewGuid().ToString();
            var list = new List<string>();

            list.AddIfNotNull(testString);

            Assert.AreEqual(1, list.Count);
            Assert.AreEqual(testString, list.Single());
        }

        [TestMethod]
        [DataRow(null)]
        [DataRow("")]
        [DataRow("  ")]
        public void AddIfNotNullTest_NullItem(string item)
        {
            var list = new List<string>();

            list.AddIfNotNull(item);

            Assert.IsFalse(list.Any());
        }

        public static IEnumerable<object[]> IsNotEmptyTestData()
        {
            yield return new object[] { null, false };
            yield return new object[] { new List<string>(), false };
            yield return new object[] { new List<string>() { "OneItem" }, true };
        }

        [TestMethod]
        [DynamicData(nameof(IsNotEmptyTestData), DynamicDataSourceType.Method)]
        public void IsNotEmptyTest(List<string> list, bool expectedResult)
        {
            var actualResult = list.IsNotEmpty();

            Assert.AreEqual(expectedResult, actualResult);
        }

        public static IEnumerable<object[]> IsNullOrEmptyTestData()
        {
            yield return new object[] { null, true };
            yield return new object[] { new List<string>(), true };
            yield return new object[] { new List<string>() { "OneItem" }, false };
        }

        [TestMethod]
        [DynamicData(nameof(IsNullOrEmptyTestData), DynamicDataSourceType.Method)]
        public void IsNullOrEmptyTest(List<string> list, bool expectedResult)
        {
            var actualResult = list.IsNullOrEmpty();

            Assert.AreEqual(expectedResult, actualResult);
        }
    }
}
