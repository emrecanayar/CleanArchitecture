using Core.Application.Requests;
using Core.Persistence.Paging;
using MediatR;
using rentACar.Application.Features.Brands.Models;
using rentACar.Application.Features.Brands.Profiles;
using rentACar.Application.Services.Repositories;
using rentACar.Domain.Entities;

namespace rentACar.Application.Features.Brands.Queries.GetListBrandPaginate
{
    public class GetListBrandPaginateQuery : IRequest<BrandListModel>
    {
        public PageRequest PageRequest { get; set; }

        public class GetListBrandPaginateQueryHandler : IRequestHandler<GetListBrandPaginateQuery, BrandListModel>
        {
            private readonly IBrandRepository _brandRepository;

            public GetListBrandPaginateQueryHandler(IBrandRepository brandRepository)
            {
                _brandRepository = brandRepository;
            }

            public async Task<BrandListModel> Handle(GetListBrandPaginateQuery request, CancellationToken cancellationToken)
            {
                IPaginate<Brand> brands = await _brandRepository.GetListAsync(index: request.PageRequest.Page, size: request.PageRequest.PageSize);
                BrandListModel mappedBrandListModel = ObjectMapper.Mapper.Map<BrandListModel>(brands);
                return mappedBrandListModel;
            }
        }
    }
}
