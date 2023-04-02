using Core.Application.Base.Queries.GetById;
using Core.Application.Base.Queries.GetList;
using Core.Application.Base.Queries.GetListByDynamic;
using Core.Application.ResponseTypes.Concrete;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using rentACar.Application.Features.Brands.Dtos;
using rentACar.Application.Features.Brands.Models;
using rentACar.Domain.Entities;

namespace rentACar.Application
{
    public static class BaseServiceRegistration
    {
        public static IServiceCollection AddBaseServices(this IServiceCollection services)
        {
            services.AddScoped<IRequestHandler<GetListQuery<Brand, BrandListModel>, CustomResponseDto<BrandListModel>>, GetListQuery<Brand, BrandListModel>.GetListQueryHandler>();
            services.AddScoped<IRequestHandler<GetByIdQuery<Brand, BrandDto>, CustomResponseDto<BrandDto>>, GetByIdQuery<Brand, BrandDto>.GetByIdQueryHandler>();
            services.AddScoped<IRequestHandler<GetListByDynamicQuery<Brand, BrandListModel>, CustomResponseDto<BrandListModel>>, GetListByDynamicQuery<Brand, BrandListModel>.GetListByDynamicQueryHandler>();

            return services;

        }
    }
}
