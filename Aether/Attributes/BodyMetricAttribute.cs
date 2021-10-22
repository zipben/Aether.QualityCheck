using System;
using System.Collections.Generic;
using System.Text;

namespace Aether.Attributes
{
    public class BodyMetricAttribute : Attribute
    {
        public Type BodyType { get; set; }
        public IEnumerable<string> Params { get; set; }

        public BodyMetricAttribute(Type bodyType, params string[] Params)
        {
            BodyType = bodyType;
            this.Params = Params;
        }
    }
}
