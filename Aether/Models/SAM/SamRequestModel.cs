using Aether.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Aether.Models.Sam
{
    public class SamRequestModel
    {
        public string CallBackUrl { get; set; }
        public string CallBackAudience { get; set; }
        public List<string> Identifiers { get; set; }
        public IdentifierType Type { get; set; }
    }
}
