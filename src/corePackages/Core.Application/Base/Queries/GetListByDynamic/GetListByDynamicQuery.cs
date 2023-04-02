using AutoMapper;
using Core.Application.Base.Rules;
using Core.Application.Requests;
using Core.Application.ResponseTypes.Concrete;
using Core.Domain.Entities.Base;
using Core.Persistence.Dynamic;
using Core.Persistence.Paging;
using Core.Persistence.Repositories;
using MediatR;
using System.Net;

namespace Core.Application.Base.Queries.GetListByDynamic
{
    public class GetListByDynamicQuery<TEntity, TModel> : IRequest<CustomResponseDto<TModel>>
      where TEntity : Entity
      where TModel : BasePageableModel
    {
        public PageRequest PageRequest { get; set; }
        public DynamicIncludeProperty DynamicIncludeProperty { get; set; }

        public class GetListByDynamicQueryHandler : IRequestHandler<GetListByDynamicQuery<TEntity, TModel>, CustomResponseDto<TModel>>
        {
            private readonly IAsyncRepository<TEntity> _asyncRepository;
            private readonly IMapper _mapper;
            private readonly BaseBusinessRules<TEntity> _baseBusinessRules;

            public GetListByDynamicQueryHandler(IAsyncRepository<TEntity> asyncRepository, IMapper mapper, BaseBusinessRules<TEntity> baseBusinessRules)
            {
                _asyncRepository = asyncRepository;
                _mapper = mapper;
                _baseBusinessRules = baseBusinessRules;
            }

            public async Task<CustomResponseDto<TModel>> Handle(GetListByDynamicQuery<TEntity, TModel> request,
                                                              CancellationToken cancellationToken)
            {
                IQueryable<TEntity> query = _asyncRepository.Query();

                if (request.DynamicIncludeProperty.IncludeProperties != null)
                {
                    IncludeSpecification<TEntity> includeSpecification = new IncludeSpecification<TEntity>();
                    foreach (string includeProperty in request.DynamicIncludeProperty.IncludeProperties)
                    {
                        includeSpecification.Include(_baseBusinessRules.GetIncludeLambda(typeof(TEntity), includeProperty));
                    }

                    query = includeSpecification.BuildQuery(query);
                }

                query = query.ToDynamic(request.DynamicIncludeProperty.Dynamic);

                IPaginate<TEntity> entities = await query.ToPaginateAsync(
                    index: request.PageRequest.Page,
                    size: request.PageRequest.PageSize
                );

                TModel mappedTModel = _mapper.Map<TModel>(entities);
                return CustomResponseDto<TModel>.Success((int)HttpStatusCode.OK, mappedTModel, isSuccess: true);
            }

        }
    }
}