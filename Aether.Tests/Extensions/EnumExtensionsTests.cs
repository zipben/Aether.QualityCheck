using System.Collections.Generic;
using Aether.Enums;
using Aether.Extensions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Aether.Tests.Extensions
{
    [TestClass]
    public class EnumExtensionsTests
    {
        public static IEnumerable<object[]> GetFriendlyDescriptionTest_EnforcementActionTypeData()
        {
            string na = "N/A";
            yield return new object[] { EnforcementActionType.ActionTaken, EnforcementType.None, na };
            yield return new object[] { EnforcementActionType.ActionNotTaken, EnforcementType.None, na };
            yield return new object[] { EnforcementActionType.NotFound, EnforcementType.None, na };
            yield return new object[] { EnforcementActionType.ActionTaken, EnforcementType.RightToAccess, na };
            yield return new object[] { EnforcementActionType.ActionNotTaken, EnforcementType.RightToAccess, na };
            yield return new object[] { EnforcementActionType.NotFound, EnforcementType.RightToAccess, na };
            yield return new object[] { EnforcementActionType.ActionTaken, EnforcementType.RightToKnow, na };
            yield return new object[] { EnforcementActionType.ActionNotTaken, EnforcementType.RightToKnow, na };
            yield return new object[] { EnforcementActionType.NotFound, EnforcementType.RightToKnow, na };
            yield return new object[] { EnforcementActionType.ActionTaken, EnforcementType.RightToDelete, "Identifiers Deleted" };
            yield return new object[] { EnforcementActionType.ActionNotTaken, EnforcementType.RightToDelete, "Identifiers Not Deleted" };
            yield return new object[] { EnforcementActionType.NotFound, EnforcementType.RightToDelete, "Identifiers Not Found" };
        }

        [TestMethod]
        [DynamicData(nameof(GetFriendlyDescriptionTest_EnforcementActionTypeData), DynamicDataSourceType.Method)]
        public void GetFriendlyDescriptionTest_EnforcementActionType(EnforcementActionType action, EnforcementType type, string expectedDescription)
        {
            Assert.AreEqual(expectedDescription, action.GetFriendlyDescription(type));
        }

        public static IEnumerable<object[]> GetFriendlyDescriptionTest_EnforcementTypeData()
        {
            yield return new object[] { EnforcementType.RightToAccess, "Right To Access" };
            yield return new object[] { EnforcementType.RightToDelete, "Right To Delete" };
            yield return new object[] { EnforcementType.RightToKnow, "Right To Know" };
            yield return new object[] { EnforcementType.None, "None" };
        }

        [TestMethod]
        [DynamicData(nameof(GetFriendlyDescriptionTest_EnforcementTypeData), DynamicDataSourceType.Method)]
        public void GetFriendlyDescriptionTest_EnforcementType(EnforcementType type, string expectedDescription)
        {
            Assert.AreEqual(expectedDescription, type.GetFriendlyDescription());
        }
    }
}
