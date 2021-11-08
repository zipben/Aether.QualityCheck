using Aether.QualityChecks.Middleware;
using Ardalis.GuardClauses;
using Microsoft.AspNetCore.Builder;
using System.Linq;

namespace Aether.QualityChecks.Extensions
{
    public static class ApplicationBuilderExtensions
    {
        /// <summary>
        /// Registers the quality check middleware, creating a ghost endpoint at the provided route.  Hitting this endpoint will result in any registered IQualityChecks being run
        /// </summary>
        /// <typeparam name="T">An optional type filter.  This is used if you want to group different tests under different endpoints, or if execution time is a concern.  This type needs to match the type
        /// provided to the IQualityCheck<T> interface.  You can type the interface with the test itself, resulting in one endpoint per quality check, or your tests can share a type, causing all like typed tests to be
        /// run together</T></typeparam>
        /// <param name="builder"></param>
        /// <param name="route">Optional overload for quality check route.  Defaults to "/api/qualitycheck"</param>
        /// <returns></returns>
        public static IApplicationBuilder UseQualityCheckMiddleware<T>(this IApplicationBuilder builder, string route = "/api/qualitycheck")
        {
            Guard.Against.InvalidInput(route, nameof(route), s => s.ElementAt(0).Equals('/'));
            return builder.UseMiddleware<QualityCheckMiddleware>(route, typeof(T));
        }

        /// <summary>
        /// Registers the quality check middleware, creating a ghost endpoint at the provided route.  Hitting this endpoint will result in any registered IQualityChecks being run
        /// </summary>
        /// <param name="builder"></param>
        /// <param name="route">Optional overload for quality check route.  Defaults to "/api/qualitycheck"</param>
        /// <returns></returns>
        public static IApplicationBuilder UseQualityCheckMiddleware(this IApplicationBuilder builder, string route = "/api/qualitycheck")
        {
            Guard.Against.InvalidInput(route, nameof(route), s => s.ElementAt(0).Equals('/'));
            return builder.UseMiddleware<QualityCheckMiddleware>(route);
        }
    }
}
