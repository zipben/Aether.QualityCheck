using System;
using System.Collections.Generic;
using System.Text;

namespace Aether.Attributes
{
    public class BodyMetricAttribute : Attribute
    {

        public string MetricName { get; set; }
        public Type BodyType { get; set; }
        public IEnumerable<string> Params { get; set; }

        public BodyMetricAttribute(string metricName, Type bodyType, params string[] par)
        {
            MetricName = metricName;
            BodyType = bodyType;
            Params = par;
        }
    }
}
