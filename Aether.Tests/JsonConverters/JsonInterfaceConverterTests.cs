using System;
using System.Collections.Generic;
using System.Text;
using Aether.Interfaces;
using Aether.JsonConverters;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;

namespace Aether.Tests.JsonConvertersTests
{
    [TestClass]
    public class JsonInterfaceConverterTests
    {
        private JsonSerializerSettings _jsonSerializationSettings;

        [TestInitialize]
        public void Init()
        {
            _jsonSerializationSettings = new JsonSerializerSettings
            {
                Converters =
                {
                    new JsonInterfaceConverter<TestSerialization, ITestSerialization>()
                }
            };
        }

        [TestMethod]
        public void SerializeTest_Object()
        {
            var testObject = new TestSerialization { Id = 1, Name = Guid.NewGuid().ToString() };

            var serializedString = JsonConvert.SerializeObject(testObject);

            var resultObject = JsonConvert.DeserializeObject<ITestSerialization>(serializedString, _jsonSerializationSettings);

            Assert.AreEqual(testObject.Id, resultObject.Id);
            Assert.AreEqual(testObject.Name, resultObject.Name);
        }

        [TestMethod]
        public void SerializeTest_List()
        {
            var testList = new List<TestSerialization>();
            for (var i = 0; i < 100; i++)
            {
                testList.Add(new TestSerialization { Id = i, Name = Guid.NewGuid().ToString() });
            };

            var serializedString = JsonConvert.SerializeObject(testList);

            var resultList = JsonConvert.DeserializeObject<List<ITestSerialization>>(serializedString, _jsonSerializationSettings);

            for (var i = 0; i < resultList.Count; i++)
            {
                Assert.AreEqual(testList[i].Id, resultList[i].Id);
                Assert.AreEqual(testList[i].Name, resultList[i].Name);
            }
        }
    }

    public interface ITestSerialization 
    {
        int Id { get; set; }
        string Name { get; set; }
    }

    public class TestSerialization : ITestSerialization 
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
