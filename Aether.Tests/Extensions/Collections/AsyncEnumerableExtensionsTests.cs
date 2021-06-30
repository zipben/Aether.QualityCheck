using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Aether.Extensions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Aether.Tests.Extensions
{
    [TestClass]
    public class AsyncEnumerableExtensionsTests
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

        [TestMethod]
        [DataRow(0)]
        [DataRow(1)]
        [DataRow(2)]
        [DataRow(TEST_LIST_COUNT)]
        public async Task ToListAsyncTest(int count)
        {
            var testAsyncEnum = GetTestData(count);

            var actualList = await testAsyncEnum.ToListAsync();

            ValidateResults(actualList, count);
        }

        [TestMethod]
        [DataRow(0)]
        [DataRow(1)]
        [DataRow(2)]
        [DataRow(TEST_LIST_COUNT)]
        public void ToEnumerableTest(int count)
        {
            var testAsyncEnum = GetTestData(count);

            var actualList = testAsyncEnum.ToEnumerable().ToList();

            ValidateResults(actualList, count);
        }

        private async IAsyncEnumerable<string> GetTestData(int maxCount)
        {
            var count = 0;
            foreach (var item in _testList)
            {
                if (count == maxCount)
                {
                    yield break;
                }
                await Task.CompletedTask;
                yield return item;
                count++;
            }
        }

        private void ValidateResults(List<string> actualList, int count)
        {
            Assert.IsNotNull(actualList);
            Assert.AreEqual(count, actualList.Count);
            foreach (var item in actualList)
            {
                Assert.IsTrue(_testList.Contains(item));
            }
        }
    }
}
