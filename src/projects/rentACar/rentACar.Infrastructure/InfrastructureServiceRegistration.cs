using Microsoft.Extensions.DependencyInjection;
using rentACar.Infrastructure.Adapters.FakeFindeksService;
using rentACar.Infrastructure.Adapters.FakePOSService;
using rentACar.Infrastructure.Adapters.ImageService;

namespace rentACar.Infrastructure
{
    public static class InfrastructureServiceRegistration
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services)
        {
            services.AddScoped<IFindeksService, FakeFindeksServiceAdapter>();
            services.AddScoped<IPOSService, FakePOSServiceAdapter>();
            services.AddScoped<IImageService, CloudinaryImageServiceAdapter>();
            return services;
        }
    }
}
