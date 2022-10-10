using Core.Application.Pipelines.Caching;
using Core.Application.Pipelines.Caching.DisturbedCache;
using Core.Application.ResponseTypes.Concrete;
using MediatR;
using Microsoft.EntityFrameworkCore;
using rentACar.Application.Features.Brands.Dtos;
using rentACar.Application.Services.Repositories;
using rentACar.Domain.Entities;
using System.Net;

namespace rentACar.Application.Features.Brands.Queries.GetListBrand
{
    public class GetListBrandQuery : IRequest<CustomResponseDto<List<BrandListDto>>>, ICachableRequest
    {
        public bool BypassCache { get; set; }
        public string CacheKey => CacheKeys.BrandList;
        public TimeSpan? SlidingExpiration { get; set; }
        public class GetListBrandQueryHandler : IRequestHandler<GetListBrandQuery, CustomResponseDto<List<BrandListDto>>>
        {
            private readonly IBrandRepository _brandRepository;

            public GetListBrandQueryHandler(IBrandRepository brandRepository)
            {
                _brandRepository = brandRepository;
            }

            public async Task<CustomResponseDto<List<BrandListDto>>> Handle(GetListBrandQuery request, CancellationToken cancellationToken)
            {
                List<Brand> brands = await _brandRepository.Query().Take(1000).ToListAsync();
                var mappedBrandList = ObjectMapper.Mapper.Map<List<BrandListDto>>(brands);
                return CustomResponseDto<List<BrandListDto>>.Success((int)HttpStatusCode.OK, data: mappedBrandList, isSuccess: true);
            }
        }
    }
}
