using System;

namespace Aether.CustomExceptions
{
    public class HealthcheckException : Exception
    {
        public HealthcheckException(string message) : base(message) { }

        public HealthcheckException(string message, Exception exception) : base(message, exception) { }
    }
}
