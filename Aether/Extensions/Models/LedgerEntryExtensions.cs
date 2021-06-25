using System.Collections.Generic;
using Aether.Interfaces;
using Aether.Interfaces.Oya;

namespace Aether.Extensions
{
    public static class LedgerEntryExtensions
    {
        public static object MakeObjectToLog(this ILedgerEntry ledgerEntry) =>
            ledgerEntry is null ? null
                                : new
                                  {
                                      ledgerEntry.DataEnforcementRequestId,
                                      ledgerEntry.RequestType,
                                      ledgerEntry.Entity,
                                      ledgerEntry.InsertionDate,
                                      ledgerEntry.Completed,
                                      ledgerEntry.CompletionDate,
                                      ledgerEntry.Confirmed,
                                      ledgerEntry.ConfirmationDate
                                  };

        public static object MakeObjectToLog(this IEnumerable<ILedgerEntry> ledgerEntryList)
        {
            if (ledgerEntryList is null)
            {
                return null;
            }

            var list = new List<object>();
            foreach (var item in ledgerEntryList)
            {
                list.Add(item.MakeObjectToLog());
            }
            return list;
        }
    }
}
