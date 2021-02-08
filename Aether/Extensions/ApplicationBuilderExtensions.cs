using Aether.Middleware;
using Microsoft.AspNetCore.Builder;

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
        public static IApplicationBuilder UseGrafanaControllerMiddleware(this IApplicationBuilder builder) =>
            builder.UseMiddleware<GrafanaControllersMiddleware>();
    }
}
