﻿using Aether.Models;
using Aether.Models.SAM;
using Aether.TestUtils.BaseClasses;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Aether.Tests.Models
{
    [TestClass]
    public class SamRequestModelTests : ModelUnitTestBase<SamRequestModel>
    {
        [TestMethod]
        public void PageParamsTest()
        {
            BaseModelTest();
        }
    }
}
