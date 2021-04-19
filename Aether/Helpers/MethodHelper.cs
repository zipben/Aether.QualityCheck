using System;
using Aether.Helpers.Interfaces;
using APILogger.Interfaces;
using Ardalis.GuardClauses;
using Microsoft.AspNetCore.Mvc;

namespace Aether.Helpers
{
    public class MethodHelper : IMethodHelper
    {
        private readonly IApiLogger _logger;

        public MethodHelper(IApiLogger logger)
        {
            Guard.Against.Null(logger, nameof(logger));
            _logger = logger;
        }

        public void Begin(out string methodNameOut, string methodName, object objectToLog = null)
        {
            methodNameOut = methodName;
            _logger.LogInfo($"Calling {methodName}", objectToLog);
        }

        public void Begin(out string methodNameOut, out object objectToLogOut, string methodName, object paramsToLog)
        {
            objectToLogOut = paramsToLog;
            Begin(out methodNameOut, methodName, paramsToLog);
        }

        public void Begin(out string methodNameOut, out object objectToLog, string methodName, Func<object> makeObjectToLog)
        {
            objectToLog = makeObjectToLog();
            Begin(out methodNameOut, methodName, objectToLog);
        }

        public void End(string methodName, object objectToLog = null)
        {
            _logger.LogInfo($"Returning from {methodName}", objectToLog);
        }

        public ActionResult EndRequestOk(string methodName, object objectToLog = null)
        {
            End(methodName, objectToLog);
            return new OkResult();
        }

        public ActionResult EndRequestOk(string methodName, string message, object objectToLog = null)
        {
            End(methodName, objectToLog);
            return new OkObjectResult(message);
        }

        public ActionResult<T> EndRequestOk<T>(string methodName, T returnObject, object objectToLog = null)
        {
            End(methodName, objectToLog);
            return new OkObjectResult(returnObject);
        }

        public ActionResult EndRequestBadRequest(string message, string parameterName, Exception exception, object objectToLog = null)
        {
            _logger.LogError($"Invalid {parameterName}: {message}", objectToLog, exception);
            return new BadRequestObjectResult($"Invalid {parameterName}");
        }

        public ActionResult EndRequestNotFound(string methodName, object objectToLog = null)
        {
            _logger.LogInfo($"Returning Not Found from {methodName}", objectToLog);
            return new NotFoundResult();
        }
    }
}
