﻿using Aether.Models.Themis;
using Aether.TestUtils.BaseClasses;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Aether.Tests.Models.Themis
{
    [TestClass]
    public class TMDSPersonTests : ModelUnitTestBase<TMDSPerson>
    {
        [TestMethod]
        public void IdentifierRootTest()
        {
            BaseModelTest();
        }
    }
}