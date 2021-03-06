using System;
using System.Collections.Generic;
using System.Text;

namespace Aether.QualityChecks.Attributes
{
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = true)]
    public class QualityCheckInitializeDataAttribute : Attribute 
    {
        public object[] Seeds { get; set; }
        public QualityCheckInitializeDataAttribute(params object[] seeds)
        {
            Seeds = seeds;
        }
    }

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

    [AttributeUsage(AttributeTargets.Method, AllowMultiple = true)]
    public class QualityCheckDataAttribute : Attribute
    {
        public object[] Parameters { get; set; }

        public QualityCheckDataAttribute(params object[] parameters)
        {
            Parameters = parameters;
        }
    }

    [AttributeUsage(AttributeTargets.Method)]
    public class QualityCheckTearDownAttribute : Attribute { }

}
