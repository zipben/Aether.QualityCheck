using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Aether.Extensions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Aether.Tests.Extensions
{
    [TestClass]
    public class EnumerableExtensionsTests
    {
        private const int TEST_LIST_COUNT = 100;
        private List<string> _testList;

        [TestInitialize]
        public void Init()
        {
            SetupTestData();
        }

        private void SetupTestData()
        {
            _testList = new List<string>();
            for (var i = 0; i < TEST_LIST_COUNT; i++)
            {
                _testList.Add(Guid.NewGuid().ToString());
            }
        }

        public static IEnumerable<object[]> HasMoreThanOneItemTestData()
        {
            yield return new object[] { null, false };
            yield return new object[] { new List<string>().AsEnumerable(), false };
            yield return new object[] { new List<string>() { "Test" }.AsEnumerable(), false };
            yield return new object[] { new List<string>() { "Test1", "Test2" }.AsEnumerable(), true };
            yield return new object[] { new List<string>() { "Test1", "Test2", "Test3" }.AsEnumerable(), true };
        }

        [TestMethod]
        [DynamicData(nameof(HasMoreThanOneItemTestData), DynamicDataSourceType.Method)]
        public void HasMoreThanOneItemTest(IEnumerable<string> testData, bool expectedResult)
        {
            var actualResult = testData.HasMoreThanOneItem();

            Assert.AreEqual(expectedResult, actualResult);
        }
    }
}
