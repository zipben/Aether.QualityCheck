using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace Aether.Extensions
{
    public static class ServiceCollectionExtensions
    {
        /// <summary>
        /// Enables Strict Transport Security header
        /// </summary>
        /// <param name="services"></param>
        public static void AddSecurityHeaders(this IServiceCollection services)
        {
            services.AddHsts(options =>
            {
                options.Preload = true;
                options.IncludeSubDomains = true;
                options.MaxAge = TimeSpan.FromDays(365);
            });
        }
    }
}
