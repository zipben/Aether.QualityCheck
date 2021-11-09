using System;
using System.Collections.Generic;
using System.Text;

namespace Aether.QualityChecks.Attributes
{
    [AttributeUsage(AttributeTargets.Method)]
    public class QualityCheckInitializeAttribute : Attribute { }

    [AttributeUsage(AttributeTargets.Method)]
    public class QualityCheckStepAttribute : Attribute 
    {
        public int Order { get; set; }

        public QualityCheckStepAttribute(int order)
        {
            Order = order;
        }
    }

    [AttributeUsage(AttributeTargets.Method)]
    public class QualityCheckTearDownAttribute : Attribute { }

}
