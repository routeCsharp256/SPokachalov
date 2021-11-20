using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Npgsql;
using OzonEdu.Infrastructure.Handlers.MerchItemAggregate;
using OzonEdu.Infrastructure.Interceptors;
using OzonEdu.MerchandiseService.Services.Interfaces;
using OzonEdu.MerchApi.Domain.AggregationModels.CustomerAggregate;
using OzonEdu.MerchApi.Domain.AggregationModels.MerchItemRequest;
using OzonEdu.MerchApi.Domain.AggregationModels.MerchPackAggregate;
using OzonEdu.MerchApi.Infrastructure.Configuration;
using OzonEdu.MerchApi.Infrastructure.DAL.Infrastructure;
using OzonEdu.MerchApi.Infrastructure.DAL.Infrastructure.Interfaces;
using OzoneEdu.MerchandiseService.GrpcServices;
using OzoneEdu.MerchandiseService.Services;

namespace OzoneEdu.MerchandiseService
{
    public sealed class Startup
    {
        public IConfiguration Configuration { get; }
        
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        public void ConfigureServices(IServiceCollection services)
        {
         
            services.AddMediatR(typeof(Startup), typeof(DatabaseConnectionOptions));
            services.AddInfrastructureServices();
            services.Configure<DatabaseConnectionOptions>(Configuration.GetSection(nameof(DatabaseConnectionOptions)));
            services.AddScoped<IDbConnectionFactory<NpgsqlConnection>, NpgsqlConnectionFactory>();
            services.AddScoped<IChangeTracker, ChangeTracker>();
            services.AddInfrastructureRepositories();
            services.AddScoped<IMerchService, MerchService>();

            services.AddGrpc(options =>
            {
                options.Interceptors.Add<ExceptionInterceptor>();
                options.Interceptors.Add<LoggingInterceptor>();
            });
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapGrpcService<MerchApiGrpcService>();
                endpoints.MapControllers();
            });
        }
    }
}