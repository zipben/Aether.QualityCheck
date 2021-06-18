using System;
using System.Collections.Generic;
using System.Text;

namespace Aether.Models
{
    public abstract class ModelBase
    {
        public List<string> DiagnosticFlags { get; set; } = new List<string>();
    }
}
