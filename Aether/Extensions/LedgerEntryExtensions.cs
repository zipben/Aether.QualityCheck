using Aether.Interfaces;

namespace Aether.Extensions
{
    public static class LedgerEntryExtensions
    {
        public static object MakeObjectToLog(this ILedgerEntry ledgerEntry) =>
            new
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
    }
}
