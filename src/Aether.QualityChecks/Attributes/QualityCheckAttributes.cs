using System;
using System.Collections.Generic;
using System.Text;

namespace Aether.QualityChecks.Attributes
{
    
    [AttributeUsage(AttributeTargets.Class)]
    public class QualityCheckFileDrivenAttribute : Attribute { }

    [AttributeUsage(AttributeTargets.Method)]
    public class QualityCheckInitializeAttribute : Attribute 
    {
        public string FileName { get; set; }

        public QualityCheckInitializeAttribute(string fileName = null)
        {
            FileName = fileName;
        }
    }

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
