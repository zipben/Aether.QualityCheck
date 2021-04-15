using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using APILogger.Interfaces;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

namespace Aether.Middleware
{
    public class ErrorHandlingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly IApiLogger _logger;

        private static HashSet<string> SECURED_ENVIRONMENTS = new HashSet<string>() { "Production", "Beta" };

        /// <summary>
        /// 
        /// </summary>
        /// <param name="logger"></param>
        /// <param name="next"></param>
        public ErrorHandlingMiddleware(IApiLogger logger, RequestDelegate next)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _next = next ?? throw new ArgumentNullException(nameof(next));
            _logger.LogDebug("Exception handling middleware initialized");
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
            catch (Exception e)
            {
                if (context.Response.HasStarted)
                {
                    _logger.LogWarning($"Error returning response: Response already started", null, e);
                    throw;
                }
                else
                {
                    _logger.LogError($"Error in the API: {e.Message}", null, e);
                    context.Response.StatusCode = 500;
                }
                context.Response.ContentType = "application/json";
                await context.Response.WriteAsync(JsonConvert.SerializeObject(GenerateErrorMessage(e.Message, e.Source, e.StackTrace)));
            }
        }

        private static Dictionary<string, string> GenerateErrorMessage(string message, string source, string stackTrace)
        {
            string environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");

            if (environment != null && !SECURED_ENVIRONMENTS.Contains(environment))
            {
                return new Dictionary<string, string>()
                {
                    {"Error", message },
                    {"Source", source },
                    {"Stack", stackTrace }
                };
            }
            else
            {
                return new Dictionary<string, string>() { { "Error", message } };
            }
        }
    }
}
