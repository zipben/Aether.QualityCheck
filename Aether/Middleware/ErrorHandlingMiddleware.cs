using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using RockLib.Logging;

namespace Aether.Middleware
{
    [ExcludeFromCodeCoverage]
    public class ErrorHandlingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger _logger;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="logger"></param>
        /// <param name="next"></param>
        public ErrorHandlingMiddleware(ILogger logger, RequestDelegate next)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _next = next ?? throw new ArgumentNullException(nameof(next));
            _logger.Debug("Exception handling middleware initialized");
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
                    _logger.Warn($"Error returning response: Response already started", e);
                    throw;
                }
                else
                {
                    _logger.Error($"Error in the Mnemosyne API: {e.Message}", e);
                    context.Response.StatusCode = 500;
                }
                context.Response.ContentType = "application/json";
                await context.Response.WriteAsync(JsonConvert.SerializeObject(GenerateErrorMessage(e.Message, e.Source, e.StackTrace)));
            }

        }

        private static Dictionary<string, string> GenerateErrorMessage(string message, string source, string stackTrace)
        {
            string environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");

            if (environment != null && !environment.Equals("Production"))
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
