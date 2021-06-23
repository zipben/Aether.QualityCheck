using Aether.Models.Themis;
using System;
using System.Collections.Generic;
using System.Text;

namespace Aether.Interfaces
{
    class ILitigation
    {
        public string Id { get; set; }
        public string CaseName { get; set; }
        public DateTime DateHoldCreated { get; set; }
        public DateTime? DateHoldEnded { get; set; }
        public List<Identifier> InputIdentifiers { get; set; }
        public List<Identifier> ResolvedIdentifiers { get; set; }
        public DateTime LastUpdateDate { get; set; }
        public DateTime CreateDate { get; set; }
    }
}
