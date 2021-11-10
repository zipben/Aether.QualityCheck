using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace Aether.QualityChecks.Exceptions
{
    public class StepFailedException : Exception 
    {
        public StepFailedException(string message) : base(message){ }
    }
}
