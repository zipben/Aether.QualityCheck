using Microsoft.VisualStudio.TestTools.UnitTesting;
using Aether.Extensions;
using System;
using System.Collections.Generic;
using System.Text;
using Aether.Interfaces;

namespace Aether.Extensions.Tests
{
    [TestClass()]
    public class DataElementClassificationExtensionsTests
    {
        private class TestDataElementClassification : IDataElementClassification
        {
            public string Entity { get; set; }
            public string Classification { get; set; }
            public Dictionary<string, string> DataElements { get; set; }
            public bool HasSSN { get; set; }
            public string Category { get; set; }
            public string MaskedValue { get; set; }
            public string ReplaceValue { get; set; }
            public string DecryptionKey { get; set; }
        }

        [TestMethod()]
        public void MakeObjectToLogNullTest()
        {
            IDataElementClassification classification = null;
            var test = classification.MakeObjectToLog();
            Assert.IsNull(test);
        }

        [TestMethod()]
        public void MakeObjectToLogValueTest()
        {
            IDataElementClassification classification = new TestDataElementClassification();
            var test = Guid.NewGuid().ToString();
            classification.Entity = test;
            var x = classification.MakeObjectToLog();
            Assert.IsNotNull(x);
        }

        [TestMethod()]
        public void MakeObjectToLogArrayNullTest()
        {
            List<IDataElementClassification> classificationEntries = null;
            var x = classificationEntries.MakeObjectToLog();
            Assert.IsNull(x);
        }

        [TestMethod()]
        public void MakeObjectToLogArrayValueTest()
        {
            IDataElementClassification classification = new TestDataElementClassification();
            var test = Guid.NewGuid().ToString();
            classification.Entity = test;
            List<IDataElementClassification> classificationEntries = new List<IDataElementClassification>();
            classificationEntries.Add(classification);
            var x = classificationEntries.MakeObjectToLog();
            Assert.IsNotNull(x);
        }
    }
}