using System;
using System.Collections.Generic;
using System.Text;

namespace Aether.Attributes
{
    public class ParamMetricAttribute : Attribute
    {
        public string MetricName { get; set; }
        public IEnumerable<string> Params { get; set; }

        public ParamMetricAttribute(string metricName, params string[] par)
        {
            MetricName = metricName;
            Params = par;
        }
    }
}
