using Core.Application.Pipelines.Authorization;
using Core.Application.Pipelines.Caching.DisturbedCache;
using Core.Application.Pipelines.Logging;
using Core.Application.Pipelines.Performance;
using Core.Application.Pipelines.Validation;
using Core.CrossCuttingConcerns.Logging.DbLog;
using Core.Integration.Base;
using Core.Integration.Dto;
using Core.Integration.Serialization;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using rentACar.Application.Features.Brands.Rules;
using rentACar.Application.Features.Documents.Rules;
using System.Reflection;

namespace rentACar.Application
{
    public static class ApplicationServiceRegistration
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {

            services.AddAutoMapper(Assembly.GetExecutingAssembly());
            services.AddMediatR(Assembly.GetExecutingAssembly());

            services.AddScoped<BrandBusinessRules>();
            services.AddScoped<DocumentBusinessRules>();
            services.AddScoped<ApiSession>();
            services.AddScoped<IJsonSerializer, JsonSerializer>();
            services.AddScoped<ILogService, LogService>();
            services.AddScoped<IBaseRestClient, BaseRestClient>();

            services.AddStackExchangeRedisCache(options =>
            {
                options.Configuration = "localhost:6379";
            });

            services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(AuthorizationBehavior<,>));
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(CachingBehavior<,>));
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(CacheRemovingBehavior<,>));
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(LoggingBehavior<,>));
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(RequestValidationBehavior<,>));
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(PerformanceBehavior<,>));

            return services;

        }
    }
}
