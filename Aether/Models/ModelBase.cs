using System.Collections.Generic;

namespace Aether.Models
{
    public abstract class ModelBase
    {
        public List<string> DiagnosticFlags { get; set; } = new List<string>();
    }
}
