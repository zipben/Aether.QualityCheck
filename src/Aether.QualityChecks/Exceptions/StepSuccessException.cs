using System;
using System.Collections.Generic;
using System.Text;

namespace Aether.QualityChecks.Exceptions
{
    public class StepSuccessException : Exception 
    {
        public object DataObject { get; set; }

        public StepSuccessException(string message, object data = null) : base(message) 
        {
            DataObject = data;
        }
    }
}
