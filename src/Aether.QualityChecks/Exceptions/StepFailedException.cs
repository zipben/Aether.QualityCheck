using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace Aether.QualityChecks.Exceptions
{
    public class StepFailedException : Exception 
    {
        public object DataObject { get; set; }

        public StepFailedException(string message, object data, Exception e) : base(message, e)
        {
            DataObject = data;
        }
    }
}
