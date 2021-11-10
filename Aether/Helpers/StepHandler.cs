using Aether.QualityChecks.Exceptions;
using System;
using System.Collections.Generic;
using System.Text;

namespace Aether.QualityChecks.Helpers
{
    public static class Step
    {
        public static void Proceed(string message = "Step Passed")
        {
            throw new StepSuccessException(message);
        }

        public static void Fail(string message = "Step Failed")
        {
            throw new StepFailedException(message);
        }
    }
}
