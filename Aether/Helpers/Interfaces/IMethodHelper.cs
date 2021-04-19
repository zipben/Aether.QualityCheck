using System;
using Microsoft.AspNetCore.Mvc;

namespace Aether.Helpers.Interfaces
{
    public interface IMethodHelper
    {
        void Begin(out string methodNameOut, out object objectToLog, string methodName, Func<object> makeObjectToLog);
        void Begin(out string methodNameOut, out object objectToLog, string methodName, object paramsToLog);
        void Begin(out string methodNameOut, string methodName, object objectToLog = null);
        void End(string methodName, object objectToLog = null);
        ActionResult EndRequestNotFound(string methodName, object objectToLog = null);
        ActionResult EndRequestOk(string methodName, object objectToLog = null);
        ActionResult EndRequestOk(string methodName, string message, object objectToLog = null);
        ActionResult<T> EndRequestOk<T>(string methodName, T returnObject, object objectToLog = null);
        ActionResult EndRequestBadRequest(string message, string parameterName, Exception exception, object objectToLog = null);
    }
}