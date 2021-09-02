using Aether.Extensions;
using Aether.ExternalAccessClients;
using Aether.ExternalAccessClients.Interfaces;
using Aether.Interfaces.ExternalAccessClients;
using Aether.Models.Configuration;
using Aether.Models.Configurations;
using Aether.Models.ErisClient;
using APILogger.Extensions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using RockLib.Metrics;
using SmokeAndMirrors.TestDependencies;

namespace SmokeAndMirrors
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddAPILogger();
            services.RegisterQualityChecks(typeof(Startup));
            services.AddSingleton<IYeOldDependencyTest, YeOldDependencyTest>();

            services.Configure<ServiceConfig>(Configuration.GetSection(nameof(ServiceConfig)));
            services.Configure<ErisConfig>(Configuration.GetSection(nameof(ErisConfig)));
            services.Configure<CreditV2Configuration>(Configuration.GetSection(nameof(CreditV2Configuration)));
            services.Configure<ConsentConfiguration>(Configuration.GetSection(nameof(ConsentConfiguration)));

            services.AddHttpClient<IHttpClientWrapper, HttpClientWrapper>();
            services.AddSingleton<IErisClient, ErisClient>();
            services.AddSingleton<ICreditV2Client, CreditV2Client>();
            services.AddSingleton<IConsentClient, ConsentClient>();

            services.RegisterAuditEventPublisher("SmokeAndMirrors");

            services.AddSingleton<IMetricFactory, MetricFactory>();

            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "SmokeAndMirrors", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "SmokeAndMirrors v1"));
            }

            app.UseQualityCheckMiddleware();

            app.UseGrafanaControllerMiddleware("/api/heartbeat", "/api/Litigation", "/api/test/*");

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
