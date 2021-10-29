using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using OzonEdu.Infrastructure.Interceptors;
using OzonEdu.MerchandiseService.Services.Interfaces;
using OzoneEdu.MerchandiseService.GrpcServices;
using OzoneEdu.MerchandiseService.Services;

namespace OzoneEdu.MerchandiseService
{
    public sealed class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton<IMerchService, MerchService>();

            services.AddGrpc(options => { options.Interceptors.Add<LoggingInterceptor>(); });
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