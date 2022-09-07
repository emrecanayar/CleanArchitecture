using Core.Application.Requests;
using Core.Persistence.Paging;
using MediatR;
using Microsoft.EntityFrameworkCore;
using rentACar.Application.Features.Models.Models;
using rentACar.Application.Services.Repositories;
using rentACar.Domain.Entities;

namespace rentACar.Application.Features.Models.Queries.GetListModelPaginate
{
    public class GetListModelPaginateQuery : IRequest<ModelListModel>
    {
        public PageRequest PageRequest { get; set; }

        public class GetListModelPaginateQueryHanlder : IRequestHandler<GetListModelPaginateQuery, ModelListModel>
        {
            private readonly IModelRepository _modelRepository;

            public GetListModelPaginateQueryHanlder(IModelRepository modelRepository)
            {
                _modelRepository = modelRepository;
            }

            public async Task<ModelListModel> Handle(GetListModelPaginateQuery request, CancellationToken cancellationToken)
            {
                IPaginate<Model> models = await _modelRepository.GetListAsync(include: m => m.Include(c => c.Brand),
                                                                              index: request.PageRequest.Page,
                                                                              size: request.PageRequest.PageSize);

                ModelListModel mappedModels = ObjectMapper.Mapper.Map<ModelListModel>(models);
                return mappedModels;
            }
        }
    }
}
