using Core.Application.Pipelines.Caching.DisturbedCache;
using Core.Application.Requests;
using Core.Persistence.Paging;
using MediatR;
using rentACar.Application.Features.Brands.Models;
using rentACar.Application.Features.Brands.Profiles;
using rentACar.Application.Services.Repositories;
using rentACar.Domain.Entities;

namespace rentACar.Application.Features.Brands.Queries.GetListBrand
{
    public class GetListBrandQuery : IRequest<BrandListModel>, ICachableRequest
    {
        public PageRequest PageRequest { get; set; }

        public bool BypassCache { get; set; }
        public string CacheKey => $"BrandList";
        public TimeSpan? SlidingExpiration { get; set; }

        public class GetListBrandQueryHander : IRequestHandler<GetListBrandQuery, BrandListModel>
        {
            private readonly IBrandRepository _brandRepository;

            public GetListBrandQueryHander(IBrandRepository brandRepository)
            {
                _brandRepository = brandRepository;
            }

            public async Task<BrandListModel> Handle(GetListBrandQuery request, CancellationToken cancellationToken)
            {
                IPaginate<Brand> brands = await _brandRepository.GetListAsync(index: request.PageRequest.Page, size: request.PageRequest.PageSize);
                BrandListModel mappedBrandListModel = ObjectMapper.Mapper.Map<BrandListModel>(brands);
                return mappedBrandListModel;
            }
        }
    }
}
