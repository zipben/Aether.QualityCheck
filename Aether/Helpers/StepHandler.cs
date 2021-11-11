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

        public static void ProceedIf(bool proceeed, string successMessage = "Step Passed", string failedMessage = "Step Failed", object successData = null, object failedData = null)
        {
            if (proceeed)
            {
                Proceed(successMessage, successData);
            }
            else
            {
                Fail(failedMessage, failedData);
            }
        }

        public static void ProceedIf(Func<bool> predicate, string successMessage = "Step Passed", string failedMessage = "Step Failed", object successData = null, object failedData = null)
        {
            if (predicate())
            {
                Proceed(successMessage, successData);
            }
            else
            {
                Fail(failedMessage, failedData);
            }
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
