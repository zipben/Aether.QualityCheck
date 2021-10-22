using System;
using System.Collections.Generic;
using System.Text;

namespace Aether.Attributes
{
    public class ParamMetricAttribute : Attribute
    {
        public IEnumerable<string> Params { get; set; }

        public ParamMetricAttribute(params string[] Params)
        {
            this.Params = Params;
        }
    }
}
