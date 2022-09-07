using Core.Application.Pipelines.Caching;
using Core.Application.Pipelines.Caching.DisturbedCache;
using MediatR;
using Microsoft.EntityFrameworkCore;
using rentACar.Application.Features.Brands.Dtos;
using rentACar.Application.Features.Brands.Models;
using rentACar.Application.Services.Repositories;
using rentACar.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace rentACar.Application.Features.Brands.Queries.GetListBrand
{
    public class GetListBrandQuery : IRequest<List<BrandListDto>>, ICachableRequest
    {
        public bool BypassCache { get; set; }
        public string CacheKey => CacheKeys.BrandList;
        public TimeSpan? SlidingExpiration { get; set; }
        public class GetListBrandQueryHandler : IRequestHandler<GetListBrandQuery, List<BrandListDto>>
        {
            private readonly IBrandRepository _brandRepository;

            public GetListBrandQueryHandler(IBrandRepository brandRepository)
            {
                _brandRepository = brandRepository;
            }

            public async Task<List<BrandListDto>> Handle(GetListBrandQuery request, CancellationToken cancellationToken)
            {
                List<Brand> brands = await _brandRepository.Query().Take(1000).ToListAsync();
                var mappedBrandList = ObjectMapper.Mapper.Map<List<BrandListDto>>(brands);
                return mappedBrandList;
            }
        }
    }
}
