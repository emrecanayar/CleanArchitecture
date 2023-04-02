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

namespace Core.Application.Base.Queries.GetList
{
    public class GetListQuery<TEntity, TModel> : IRequest<CustomResponseDto<TModel>>
     where TEntity : Entity
     where TModel : BasePageableModel
    {
        public PageRequest PageRequest { get; set; }
        public IncludeProperty IncludeProperty { get; set; }
        public class GetListQueryHandler : IRequestHandler<GetListQuery<TEntity, TModel>, CustomResponseDto<TModel>>
        {
            private readonly IRepository<TEntity> _repository;
            private readonly IAsyncRepository<TEntity> _asyncRepository;
            private readonly IMapper _mapper;
            private readonly BaseBusinessRules<TEntity> _baseBusinessRules;

            public GetListQueryHandler(IRepository<TEntity> repository, IAsyncRepository<TEntity> asyncRepository, IMapper mapper, BaseBusinessRules<TEntity> baseBusinessRules)
            {
                _repository = repository;
                _asyncRepository = asyncRepository;
                _mapper = mapper;
                _baseBusinessRules = baseBusinessRules;
            }

            public async Task<CustomResponseDto<TModel>> Handle(GetListQuery<TEntity, TModel> request,
                                                              CancellationToken cancellationToken)
            {
                IQueryable<TEntity> query = _asyncRepository.Query();

                if (request.IncludeProperty.IncludeProperties != null)
                {
                    IncludeSpecification<TEntity> includeSpecification = new IncludeSpecification<TEntity>();
                    foreach (string includeProperty in request.IncludeProperty.IncludeProperties)
                    {
                        includeSpecification.Include(_baseBusinessRules.GetIncludeLambda(typeof(TEntity), includeProperty));
                    }

                    query = includeSpecification.BuildQuery(query);
                }

                IPaginate<TEntity> entities = await query.ToPaginateAsync(index: request.PageRequest.Page, size: request.PageRequest.PageSize);
                TModel mappedTModel = _mapper.Map<TModel>(entities);
                return CustomResponseDto<TModel>.Success((int)HttpStatusCode.OK, mappedTModel, isSuccess: true);
            }

        }
    }
}

