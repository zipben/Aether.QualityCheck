using Aether.Extensions;
using Aether.QualityChecks.Helpers;
using Aether.QualityChecks.Interfaces;
using Aether.QualityChecks.Middleware;
using Aether.QualityChecks.TestRunner;
using Ardalis.GuardClauses;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
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
        public static void UseQualityCheckMiddleware<T>(this IApplicationBuilder app, string route = "/api/qualitycheck", bool bypassDependencyValidation = false)
        {

            PathString routePs = new PathString(route);

            if (!bypassDependencyValidation) 
            {
                if (app.ApplicationServices.GetService(typeof(IQualityCheckExecutionHandler)) == null)
                {
                    throw new InvalidOperationException($"Unable to Find {nameof(IQualityCheckExecutionHandler)} - Consider using the {nameof(ServiceCollectionExtensions.RegisterQualityChecks)} extension method");
                }

                if (app.ApplicationServices.GetService(typeof(IQualityCheckRunner)) == null)
                {
                    throw new InvalidOperationException($"Unable to Find {nameof(IQualityCheckRunner)} - Consider using the {nameof(ServiceCollectionExtensions.RegisterQualityChecks)} extension method");
                }
            }


            // NOTE: we explicitly don't use Map here because it's really common for multiple quality
            // check middleware to overlap in paths. Ex: `/quality`, `/quality/detailed` - this is order
            // sensitive with Map, and it's really surprising to people.
            //
            // See:
            // https://github.com/aspnet/Diagnostics/issues/511
            // https://github.com/aspnet/Diagnostics/issues/512
            // https://github.com/aspnet/Diagnostics/issues/514

            Func<HttpContext, bool> predicate = c =>
            {
                return (// If you do provide a PathString, want to handle all of the special cases that
                        // StartsWithSegments handles, but we also want it to have exact match semantics.
                        //
                        // Ex: /Foo/ == /Foo (true)
                        // Ex: /Foo/Bar == /Foo (false)
                        (c.Request.Path.StartsWithSegments(routePs, out var remaining) &&
                        string.IsNullOrEmpty(remaining)));
            };

            app.MapWhen(predicate, b => b.UseMiddleware<QualityCheckMiddleware>(typeof(T)));
        }

        /// <summary>
        /// Registers the quality check middleware, creating a ghost endpoint at the provided route.  Hitting this endpoint will result in any registered IQualityChecks being run
        /// </summary>
        /// <param name="builder"></param>
        /// <param name="route">Optional overload for quality check route.  Defaults to "/api/qualitycheck"</param>
        /// <returns></returns>
        public static void UseQualityCheckMiddleware(this IApplicationBuilder app, string route = "/api/qualitycheck", bool bypassDependencyValidation = false)
        {
            PathString routePs = new PathString(route);

            if (!bypassDependencyValidation)
            {
                if (app.ApplicationServices.GetService(typeof(IQualityCheckExecutionHandler)) == null)
                {
                    throw new InvalidOperationException($"Unable to Find {nameof(IQualityCheckExecutionHandler)} - Consider using the {nameof(ServiceCollectionExtensions.RegisterQualityChecks)} extension method");
                }

                if (app.ApplicationServices.GetService(typeof(IQualityCheckRunner)) == null)
                {
                    throw new InvalidOperationException($"Unable to Find {nameof(IQualityCheckRunner)} - Consider using the {nameof(ServiceCollectionExtensions.RegisterQualityChecks)} extension method");
                }
            }

            // NOTE: we explicitly don't use Map here because it's really common for multiple quality
            // check middleware to overlap in paths. Ex: `/quality`, `/quality/detailed` - this is order
            // sensitive with Map, and it's really surprising to people.
            //
            // See:
            // https://github.com/aspnet/Diagnostics/issues/511
            // https://github.com/aspnet/Diagnostics/issues/512
            // https://github.com/aspnet/Diagnostics/issues/514

            Func<HttpContext, bool> predicate = c =>
            {
                return (// If you do provide a PathString, want to handle all of the special cases that
                        // StartsWithSegments handles, but we also want it to have exact match semantics.
                        //
                        // Ex: /Foo/ == /Foo (true)
                        // Ex: /Foo/Bar == /Foo (false)
                        (c.Request.Path.StartsWithSegments(routePs, out var remaining) &&
                        string.IsNullOrEmpty(remaining)));
            };

            app.MapWhen(predicate, b => b.UseMiddleware<QualityCheckMiddleware>());
        }
    }
}
