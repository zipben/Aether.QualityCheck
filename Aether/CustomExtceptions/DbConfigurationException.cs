using System;
using System.Diagnostics.CodeAnalysis;

namespace Aether.CustomExceptions
{
    [ExcludeFromCodeCoverage]
    public class DbConfigurationException: Exception
    {
        public DbConfigurationException(string functionName, Exception triggeringException)
            : base($"Db encountered exception while trying to execute:{functionName} - {triggeringException.Message}")
        {
            this.Data.Add("TriggeringException", triggeringException);
        }
    }
}
