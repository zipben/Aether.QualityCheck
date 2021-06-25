using Ardalis.GuardClauses;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;

namespace Aether.Tests.GuardClauseExtensionTests
{
    [TestClass]
    public class DictionaryGuardTests
    {
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void MissingKey_ThrowsException()
        {
            Dictionary<string, string> testDictionary = new Dictionary<string, string>();

            Guard.Against.MissingKey(testDictionary, "TestKey", "ParamName");
        }

        [TestMethod]
        public void MissingKey_HasKey_NoException()
        {
            Dictionary<string, string> testDictionary = new Dictionary<string, string>();
            testDictionary.Add("TestKey", "fun");

            Guard.Against.MissingKey(testDictionary, "TestKey", "ParamName");
        }

    }
}
