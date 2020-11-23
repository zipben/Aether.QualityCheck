using System.Diagnostics.CodeAnalysis;
using Aether.Middleware;
using Microsoft.AspNetCore.Builder;

namespace Aether.Extensions
{
    [ExcludeFromCodeCoverage]
    public static class ExceptionHandlingMiddlewareExtensions
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="builder"></param>
        /// <returns></returns>
        public static IApplicationBuilder UseExceptionHandlingMiddleware(this IApplicationBuilder builder) =>
            builder.UseMiddleware<ErrorHandlingMiddleware>();
    }
}
