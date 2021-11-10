using Aether.QualityChecks.Exceptions;
using System;
using System.Collections.Generic;
using System.Text;

namespace Aether.QualityChecks.Helpers
{
    public static class Step
    {
        public static void Proceed(string message = "Step Passed", object data = null)
        {
            throw new StepSuccessException(message, data);
        }

        public static void Fail(string message = "Step Failed", object data = null, Exception e = null)
        {
            throw new StepFailedException(message, data, e);
        }

        public static void Warn(string message = "Warning", object data = null, Exception e = null)
        {
            throw new StepWarnException(message, data, e);
        }
    }
}
