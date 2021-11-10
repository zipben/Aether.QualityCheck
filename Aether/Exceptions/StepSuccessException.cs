using System;
using System.Collections.Generic;
using System.Text;

namespace Aether.QualityChecks.Exceptions
{
    public class StepSuccessException : Exception 
    {
        public StepSuccessException(string message) : base(message) { }
    }
}
