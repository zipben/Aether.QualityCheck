using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace Aether.QualityChecks.Exceptions
{
    public class StepWarnException : Exception 
    {
        public StepWarnException(string message, Exception e) : base(message, e){ }
    }
}
