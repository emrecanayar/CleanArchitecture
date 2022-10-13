using Core.Application.Pipelines.Logging;
using Core.Application.Requests;
using Core.Application.ResponseTypes.Concrete;
using Core.Persistence.Paging;
using MediatR;
using rentACar.Application.Features.Brands.Models;
using rentACar.Application.Services.Repositories;
using rentACar.Domain.Entities;
using System.Net;

namespace rentACar.Application.Features.Brands.Queries.GetListBrandPaginate
{
    public class GetListBrandPaginateQuery : IRequest<CustomResponseDto<BrandListModel>>, ILoggableRequest
    {
        public PageRequest PageRequest { get; set; }

        public class GetListBrandPaginateQueryHandler : IRequestHandler<GetListBrandPaginateQuery, CustomResponseDto<BrandListModel>>
        {
            private readonly IBrandRepository _brandRepository;

            public GetListBrandPaginateQueryHandler(IBrandRepository brandRepository)
            {
                _brandRepository = brandRepository;
            }

            public async Task<CustomResponseDto<BrandListModel>> Handle(GetListBrandPaginateQuery request, CancellationToken cancellationToken)
            {
                IPaginate<Brand> brands = await _brandRepository.GetListAsync(index: request.PageRequest.Page, size: request.PageRequest.PageSize);
                BrandListModel mappedBrandListModel = ObjectMapper.Mapper.Map<BrandListModel>(brands);
                return CustomResponseDto<BrandListModel>.Success((int)HttpStatusCode.OK, data: mappedBrandListModel, isSuccess: true);
            }
        }
    }
}