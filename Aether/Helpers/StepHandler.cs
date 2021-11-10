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

        public static void Fail(string message = "Step Failed", Exception e = null)
        {
            throw new StepFailedException(message, e);
        }

        public static void Warn(string message = "Warning", Exception e = null)
        {
            throw new StepWarnException(message, e);
        }
    }
}
