using Core.Application.Requests;
using Core.Application.ResponseTypes.Concrete;
using Core.Persistence.Dynamic;
using Core.Persistence.Paging;
using MediatR;
using Microsoft.EntityFrameworkCore;
using rentACar.Application.Features.Models.Models;
using rentACar.Application.Services.Repositories;
using rentACar.Domain.Entities;
using System.Net;

namespace rentACar.Application.Features.Models.Queries.GetListModelByDynamic
{
    public class GetListModelByDynamicQuery : IRequest<CustomResponseDto<ModelListModel>>
    {
        public Dynamic Dynamic { get; set; }
        public PageRequest PageRequest { get; set; }


        public class GetListModelByDynamicQueryHandler : IRequestHandler<GetListModelByDynamicQuery, CustomResponseDto<ModelListModel>>
        {
            private readonly IModelRepository _modelRepository;

            public GetListModelByDynamicQueryHandler(IModelRepository modelRepository)
            {
                _modelRepository = modelRepository;
            }

            public async Task<CustomResponseDto<ModelListModel>> Handle(GetListModelByDynamicQuery request, CancellationToken cancellationToken)
            {
                IPaginate<Model> models = await _modelRepository.GetListByDynamicAsync(dynamic: request.Dynamic,
                                                                                       include: m => m.Include(c => c.Brand),
                                                                                       index: request.PageRequest.Page,
                                                                                       size: request.PageRequest.PageSize);

                ModelListModel mappedModels = ObjectMapper.Mapper.Map<ModelListModel>(models);
                return CustomResponseDto<ModelListModel>.Success((int)HttpStatusCode.OK, mappedModels, isSuccess: true);
            }
        }
    }
}
