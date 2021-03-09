using Microsoft.VisualStudio.TestTools.UnitTesting;
using Aether.Helpers;
using System;
using System.Collections.Generic;
using System.Text;
using Aether.Enums;

namespace Aether.Helpers.Tests
{
    [TestClass()]
    public class EnumHelpersTests
    {
        [TestMethod()]
        public void GetFriendlyNameTest_EnforcementActionType_ActionNotTaken()
        {
            Assert.AreEqual("Action Not Taken", EnumHelpers.GetFriendlyName(EnforcementActionType.ActionNotTaken));
        }

        [TestMethod()]
        public void GetFriendlyNameTest_EnforcementType_RightToDelete()
        {
            Assert.AreEqual("Right To Delete", EnumHelpers.GetFriendlyName(EnforcementType.RightToDelete));
        }

        [TestMethod()]
        public void GetFriendlyNameTest_IdentifierType_ActionNotTaken()
        {
            Assert.AreEqual("Phone Number", EnumHelpers.GetFriendlyName(IdentifierType.PhoneNumber));
        }
    }
}