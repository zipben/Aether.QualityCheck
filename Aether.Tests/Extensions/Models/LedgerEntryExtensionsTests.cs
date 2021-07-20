using Microsoft.VisualStudio.TestTools.UnitTesting;
using Aether.Extensions;
using System;
using System.Collections.Generic;
using System.Text;
using Aether.Interfaces.Oya;
using Moq;
using Aether.Enums;

namespace Aether.Extensions.Tests
{
    [TestClass()]
    public class LedgerEntryExtensionsTests
    {
        private class TestLedger : ILedgerEntry
        {
            public string DataEnforcementRequestId { get; set; }
            public string Entity { get; set; }
            public EnforcementType RequestType { get; set; }
            public Dictionary<string, List<string>> PersonalData { get; set; }
            public bool Confirmed { get; set; }
            public bool Completed { get; set; }
            public long? ConfirmationDate { get; set; }
            public long InsertionDate { get; set; }
            public long ExpirationDate { get; set; }
            public long? CompletionDate { get; set; }
            public bool? IsTest { get; set; }
            public EnforcementActionType? ActionType { get; set; }
            public string ConfirmedBy { get; set; }
        }
        [TestMethod()]
        public void MakeObjectToLogNullTest()
        {
            ILedgerEntry ledger = null;
            var test = ledger.MakeObjectToLog();
            Assert.IsNull(test);
        }

        [TestMethod()]
        public void MakeObjectToLogValueTest()
        {
            ILedgerEntry ledger = new TestLedger();
            var test = Guid.NewGuid().ToString();
            ledger.Entity = test;
            var x = ledger.MakeObjectToLog();
            Assert.IsNotNull(x);
        }

        [TestMethod()]
        public void MakeObjectToLogArrayNullTest()
        {
            List<ILedgerEntry> ledgerEntries = null;
            var x = ledgerEntries.MakeObjectToLog();
            Assert.IsNull(x);
        }

        [TestMethod()]
        public void MakeObjectToLogArrayValueTest()
        {
            ILedgerEntry ledger = new TestLedger();
            var test = Guid.NewGuid().ToString();
            ledger.Entity = test;
            List<ILedgerEntry> ledgerEntries = new List<ILedgerEntry>();
            ledgerEntries.Add(ledger);
            var x = ledgerEntries.MakeObjectToLog();
            Assert.IsNotNull(x);
        }
    }
}