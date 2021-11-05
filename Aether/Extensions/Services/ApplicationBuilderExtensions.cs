using Aether.Middleware;
using Ardalis.GuardClauses;
using Microsoft.AspNetCore.Builder;
using System.Diagnostics.CodeAnalysis;
using System.Linq;

namespace Aether.Extensions
{
    public static class ApplicationBuilderExtensions
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="builder"></param>
        /// <returns></returns>
        public static IApplicationBuilder UseExceptionHandlingMiddleware(this IApplicationBuilder builder) =>
            builder.UseMiddleware<ErrorHandlingMiddleware>();
        
        public static IApplicationBuilder UseGrafanaControllerMiddleware(this IApplicationBuilder builder, params string[] filterLists)
        {
            if (!filterLists.Any())
                filterLists = new string[] { "/api/heartbeat" };

            return builder.UseMiddleware<GrafanaControllersMiddleware>(filterLists.ToList());
        }

        public static IApplicationBuilder UseMoriaMetricsMiddleware(this IApplicationBuilder builder, params string[] filterLists)
        {
            if (!filterLists.Any())
                filterLists = new string[] { "/api/heartbeat", "/api/QualityCheck" };

            return builder.UseMiddleware<MoriaMetricsMiddleware>(filterLists.ToList());
        }

        public static IApplicationBuilder UseQualityCheckMiddleware(this IApplicationBuilder builder, string route = "/api/QualityCheck")
        {
            Guard.Against.InvalidInput(route, nameof(route), s => s.ElementAt(0).Equals('/'));

            return builder.UseMiddleware<QualityCheckMiddleware>(route);
        }

        /// <summary>
        /// Adds Strict Transport Security and XSS Protection headers to responses
        /// </summary>
        /// <param name="builder"></param>
        /// <param name="isDevelopment"></param>
        [ExcludeFromCodeCoverage]
        public static void UseSecurityHeaders(this IApplicationBuilder builder, bool isDevelopment)
        {
            if (!isDevelopment)
            {
                builder.UseHsts();
            }

            builder.UseHttpsRedirection();

            builder.Use(async (context, next) =>
            {
                context.Response.Headers.Add("X-Xss-Protection", "1");
                await next();
            });
        }
    }
}
