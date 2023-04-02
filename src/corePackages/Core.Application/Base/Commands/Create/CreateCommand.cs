using AutoMapper;
using Core.Application.ResponseTypes.Concrete;
using Core.Domain.Entities.Base;
using Core.Persistence.Repositories;
using MediatR;
using System.Net;

namespace Core.Application.Base.Commands.Create
{
    public class CreateCommand<TEntity, TModel> : IRequest<CustomResponseDto<TModel>>
    where TEntity : Entity
    where TModel : class
    {
        public TModel Model { get; set; }

        public CreateCommand(TModel model)
        {
            Model = model;
        }

        public class CreateCommandHandler : IRequestHandler<CreateCommand<TEntity, TModel>, CustomResponseDto<TModel>>
        {
            private readonly IAsyncRepository<TEntity> _asyncRepository;
            private readonly IMapper _mapper;

            public CreateCommandHandler(IAsyncRepository<TEntity> asyncRepository, IMapper mapper)
            {
                _asyncRepository = asyncRepository;
                _mapper = mapper;
            }

            public async Task<CustomResponseDto<TModel>> Handle(CreateCommand<TEntity, TModel> request, CancellationToken cancellationToken)
            {
                TEntity entity = _mapper.Map<TEntity>(request.Model);
                TEntity createdEntity = await _asyncRepository.AddAsync(entity);
                TModel createdModel = _mapper.Map<TModel>(createdEntity);

                return CustomResponseDto<TModel>.Success((int)HttpStatusCode.Created, createdModel, isSuccess: true);
            }
        }
    }

}
