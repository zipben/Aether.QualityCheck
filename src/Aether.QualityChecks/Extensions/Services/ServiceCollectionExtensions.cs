using Aether.QualityChecks.Helpers;
using Aether.QualityChecks.Interfaces;
using Aether.QualityChecks.TestRunner;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using System;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Reflection;

namespace Aether.Extensions
{
    [ExcludeFromCodeCoverage]
    public static class ServiceCollectionExtensions
    {
        /// <summary>
        /// Register all classes that impliment the IQualityCheck interface.  Any new tests will be registered automatically
        /// </summary>
        /// <typeparam name="T">A type the shares the same assembly as the quality checks you are registering
        /// The convention is to use Startup</typeparam>
        /// <param name="services"></param>
        public static void RegisterQualityChecks<T>(this IServiceCollection services, ServiceLifetime lifeTime = ServiceLifetime.Singleton)
        {
            services.Add(new ServiceDescriptor(typeof(IQualityCheckRunner), typeof(QualityCheckRunner), lifeTime));
            services.Add(new ServiceDescriptor(typeof(IQualityCheckExecutionHandler), typeof(QualityCheckExecutionHandler), lifeTime));

            Assembly[] assemblies = new[] { typeof(T).Assembly };

            var typesFromAssemblies = assemblies.SelectMany(a => a.DefinedTypes.Where(x => x.GetInterfaces().Contains(typeof(IQualityCheck))));

            foreach (var type in typesFromAssemblies)
                services.Add(new ServiceDescriptor(typeof(IQualityCheck), type, lifeTime));
        }
    }
}
