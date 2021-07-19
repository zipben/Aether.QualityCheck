using System;
using System.Collections.Generic;
using Aether.Interfaces;
using Aether.Interfaces.Themis;

namespace Aether.Models.Themis
{
    public class Litigation : ILitigation
    {
        public string Id { get; set; }
        public string CaseName { get; set; }
        public DateTime DateHoldCreated { get; set; }
        public DateTime? DateHoldEnded { get; set; }
        public List<IIdentifier> InputIdentifiers { get; set; }
        public List<IIdentifier> ResolvedIdentifiers { get; set; }
        public DateTime LastUpdateDate { get; set; }
        public DateTime CreateDate { get; set; }
    }
}
