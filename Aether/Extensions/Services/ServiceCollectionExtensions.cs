using System;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Reflection;
using Aether.ExternalAccessClients;
using Aether.ExternalAccessClients.Interfaces;
using Aether.Interfaces;
using Aether.Models.Configuration;
using Ardalis.GuardClauses;
using Confluent.Kafka;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Aether.Extensions
{
    [ExcludeFromCodeCoverage]
    public static class ServiceCollectionExtensions
    {
        private const string NEWTONSOFT_LICENSE = "Newtonsoft_License";

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

        /// <summary>
        /// Adds all non abstract services of type specified in TService found in assembly TSource
        /// </summary>
        /// <typeparam name="TService">Type of services to register</typeparam>
        /// <typeparam name="TSource">Root level assembly.  Typically Startup</typeparam>
        /// <param name="services"></param>
        /// <param name="lifeTime">Optional Service Lifetime Definition.  Defaults to Singleton</param>
        public static void AddAll<TService, TSource>(this IServiceCollection services, ServiceLifetime lifeTime = ServiceLifetime.Singleton)
        {
            Type sourceType = typeof(TSource);

            Assembly[] assemblies = new[] { sourceType.Assembly };

            var typesFromAssemblies = assemblies.SelectMany(a => a.DefinedTypes.Where(x => x.GetInterfaces().Contains(typeof(TService))));

            foreach (var type in typesFromAssemblies)
            {
                if (!type.IsAbstract)
                {
                    services.Add(new ServiceDescriptor(typeof(TService), type, lifeTime));
                }
            }
        }

        public static void RegisterQualityChecks(this IServiceCollection services, Type sourceType)
        {
            Assembly[] assemblies = new[] { sourceType.Assembly };

            var typesFromAssemblies = assemblies.SelectMany(a => a.DefinedTypes.Where(x => x.GetInterfaces().Contains(typeof(IQualityCheck))));

            foreach (var type in typesFromAssemblies)
                services.Add(new ServiceDescriptor(typeof(IQualityCheck), type, ServiceLifetime.Singleton));
        }

        public static void RegisterKafkaServices(this IServiceCollection services, IConfiguration configuration)
        {
            var section =       Guard.Against.MissingConfigurationSection(configuration, nameof(KafkaSettings));

            var hostName =      Guard.Against.MissingConfigurationValue(section, nameof(KafkaSettings.HostName));
            var password =      Guard.Against.MissingConfigurationValue(section, nameof(KafkaSettings.Password));
            var userName =      Guard.Against.MissingConfigurationValue(section, nameof(KafkaSettings.UserName));
            var topic =         Guard.Against.MissingConfigurationValue(section, nameof(KafkaSettings.Topic));

            var jsonLicense =   Guard.Against.MissingConfigurationValue(configuration, NEWTONSOFT_LICENSE);

            services.AddSingleton<IKafkaNotifier, KafkaNotifier>();
            services.AddSingleton<IProducer<string, string>>(x =>
            {
                var producerConfig = new ProducerConfig
                {
                    BootstrapServers =                      hostName,
                    SaslUsername =                          userName,
                    SaslPassword =                          password,
                    EnableSslCertificateVerification =      false,
                    SaslMechanism =                         SaslMechanism.Plain,
                    SecurityProtocol =                      SecurityProtocol.SaslSsl,
                    SslEndpointIdentificationAlgorithm =    SslEndpointIdentificationAlgorithm.Https
                };
                return new ProducerBuilder<string, string>(producerConfig).Build();
            });

            // License for JSON Schema Validation
            Newtonsoft.Json.Schema.License.RegisterLicense(jsonLicense);
        }
    }
}
