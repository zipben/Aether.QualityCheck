using System;
using System.Linq;
using System.Reflection;
using Aether.Interfaces;
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

        public static void RegisterQualityChecks(this IServiceCollection services, Type sourceType)
        {
            Assembly[] assemblies = new[] { sourceType.Assembly };

            var typesFromAssemblies = assemblies.SelectMany(a => a.DefinedTypes.Where(x => x.GetInterfaces().Contains(typeof(IQualityCheck))));

            foreach (var type in typesFromAssemblies)
                services.Add(new ServiceDescriptor(typeof(IQualityCheck), type, ServiceLifetime.Singleton));
        }
    }
}
