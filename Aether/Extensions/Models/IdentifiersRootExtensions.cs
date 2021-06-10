using Aether.Enums;
using Aether.Models.ErisClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Aether.Extensions.Models
{
    public static class IdentifiersRootExtensions
    {
        public static List<string> AggregateCorrelatedIdentifierByIdentifier(this IdentifiersRoot identifiersRoot, IdentifierType identifier)
        {
            var searchKey = identifier.ToString().ToUpper();

            var gcids = from id in identifiersRoot.Identifiers
                        from cor in id.CorrelatedIdentifiers
                        where cor.Key.ToUpper().Contains(searchKey)
                        select cor.Value;

            var theBits = gcids.SelectMany(x => x);

            return theBits.ToList();
        }
    }
}
