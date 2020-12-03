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
        public async Task ToListAsyncTest()
        {
            var testAsyncEnum = GetTestData();

            var actualList = await testAsyncEnum.ToListAsync();

            ValidateResults(actualList);
        }

        [TestMethod]
        public void ToEnumerableTest()
        {
            var testAsyncEnum = GetTestData();

            var actualList = testAsyncEnum.ToEnumerable().ToList();

            ValidateResults(actualList);
        }

        private async IAsyncEnumerable<string> GetTestData()
        {
            foreach (var item in _testList)
            {
                await Task.CompletedTask;
                yield return item;
            }
        }

        private void ValidateResults(List<string> actualList)
        {
            Assert.IsNotNull(actualList);
            Assert.AreEqual(_testList.Count, actualList.Count);
            foreach (var item in actualList)
            {
                Assert.IsTrue(_testList.Contains(item));
            }
        }
    }
}
