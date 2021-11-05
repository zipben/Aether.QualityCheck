using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using Aether.Attributes;
using Aether.CustomExtceptions;
using APILogger.Interfaces;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

namespace Aether.Middleware
{
    public class ErrorHandlingMiddleware : MiddlewareBase
    {
        private static HashSet<string> SECURED_ENVIRONMENTS = new HashSet<string>() { "Production", "Beta" };

        /// <summary>
        /// 
        /// </summary>
        /// <param name="logger"></param>
        /// <param name="next"></param>
        public ErrorHandlingMiddleware(IApiLogger logger, RequestDelegate next) : base(logger, next)
        {
            _logger.LogDebug($"{nameof(ErrorHandlingMiddleware)} initialized");
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (ArgumentException ex)
            {
                await HandleException(ex, context, HttpStatusCode.BadRequest, $"Invalid {ex.ParamName}: {ex.Message}");
            }
            catch(HttpStatusCodeException ex)
            {
                await HandleException(ex, context, ex.StatusCode, $"Error in the API: {ex.Message}");
            }
            catch (Exception ex)
            {
                await HandleException(ex, context, HttpStatusCode.InternalServerError, $"Error in the API: {ex.Message}");
            }
        }

        private async Task HandleException(Exception ex, HttpContext context, HttpStatusCode statusCode, string logMessage)
        {
            if (context.Response.HasStarted)
            {
                _logger.LogWarning($"Error returning response: Response already started", null, ex);
                throw ex;
            }
            else
            {
                _logger.LogError(logMessage, null, ex);
                context.Response.StatusCode = (int) statusCode;
            }

            context.Response.ContentType = "application/json";
            await context.Response.WriteAsync(JsonConvert.SerializeObject(GenerateErrorMessage(ex.Message, ex.Source, ex.StackTrace)));
        }

        private static Dictionary<string, string> GenerateErrorMessage(string message, string source, string stackTrace)
        {
            string environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");

            var dict = new Dictionary<string, string>() { {"Error", message } };

            if (environment != null && !SECURED_ENVIRONMENTS.Contains(environment))
            {
                dict.Add("Source", source);
                dict.Add("Stack", stackTrace);
            }

            return dict;
        }
    }
}
