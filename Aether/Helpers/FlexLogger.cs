using Aether.Enums;
using Amazon.Lambda.Core;
using APILogger.Interfaces;
using Ardalis.GuardClauses;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Aether.Helpers
{
    internal class FlexLogger
    {
        

        private readonly IApiLogger _ecsLogger;
        private readonly ServiceType _serviceType;

        public FlexLogger(IApiLogger ecsLogger, ServiceType serviceType)
        {
            if (serviceType == ServiceType.ECS)
                _ecsLogger = Guard.Against.Null(ecsLogger, nameof(ecsLogger), "ECS Logger Cannot be Null when using flex logger in ECS mode");
            else
                _ecsLogger = null;
        }

        ///<inheritdoc/>
        public void LogError(string message, object obj = null, Exception exception = null)
        {
            switch (_serviceType)
            {
                case ServiceType.ECS: _ecsLogger.LogError(message, obj, exception); break;
                case ServiceType.Lambda: LambdaLog(nameof(LogError), message, obj, exception); break;
            }
        }

        ///<inheritdoc/>
        public void LogWarning(string message, object obj = null, Exception exception = null)
        {
            switch (_serviceType)
            {
                case ServiceType.ECS: _ecsLogger.LogWarning(message, obj, exception); break;
                case ServiceType.Lambda: LambdaLog(nameof(LogWarning), message, obj, exception); break;
            }
        }

        ///<inheritdoc/>
        public void LogFatal(string message, object obj = null, Exception exception = null) 
        {
            switch (_serviceType)
            {
                case ServiceType.ECS: _ecsLogger.LogFatal(message, obj, exception); break;
                case ServiceType.Lambda: LambdaLog(nameof(LogFatal), message, obj, exception); break;
            }
        }

        ///<inheritdoc/>
        public void LogDebug(string message, object obj = null)
        {
            switch (_serviceType)
            {
                case ServiceType.ECS: _ecsLogger.LogDebug(message, obj); break;
                case ServiceType.Lambda: LambdaLog(nameof(LogDebug), message, obj); break;
            }
        }

        ///<inheritdoc/>
        public void LogInfo(string message, object obj = null)
        {
            switch (_serviceType)
            {
                case ServiceType.ECS: _ecsLogger.LogInfo(message, obj); break;
                case ServiceType.Lambda: LambdaLog(nameof(LogInfo), message, obj); break;
            }
        }

        ///<inheritdoc/>
        public void LogAudit(string message, object obj = null)
        {
            switch (_serviceType)
            {
                case ServiceType.ECS: _ecsLogger.LogAudit(message, obj); break;
                case ServiceType.Lambda: LambdaLog(nameof(LogAudit), message, obj); break;
            }
        }

        private void LambdaLog(string prepend, string message, object logObject = null, Exception exception = null)
        {
            try
            {
                Dictionary<string, string> logComponents = new Dictionary<string, string>();

                logComponents.Add(nameof(message), message);

                if (logObject != null)
                    logComponents.Add(nameof(logObject), JsonConvert.SerializeObject(logObject));

                if (exception != null)
                    logComponents.Add(nameof(exception), JsonConvert.SerializeObject(exception));

                LambdaLogger.Log($"{prepend}::::{JsonConvert.SerializeObject(logComponents)}");
            }
            catch
            {
                Console.WriteLine("Encountered error while attempting to log to cloudwatch from Flex Logger");
            }
        }
    }
}
