using AutoMapper;
using Core.Application.Base.Rules;
using Core.Application.ResponseTypes.Concrete;
using Core.Domain.Entities.Base;
using Core.Persistence.Repositories;
using MediatR;
using System.Net;

namespace Core.Application.Base.Commands.Update
{
    public class UpdateCommand<TEntity, TModel> : IRequest<CustomResponseDto<TModel>>
     where TEntity : Entity
     where TModel : IEntityModel
    {
        public TModel Model { get; set; }

        public UpdateCommand(TModel model)
        {
            Model = model;
        }

        public class UpdateCommandHandler : IRequestHandler<UpdateCommand<TEntity, TModel>, CustomResponseDto<TModel>>
        {
            private readonly IAsyncRepository<TEntity> _asyncRepository;
            private readonly IMapper _mapper;
            private readonly BaseBusinessRules<TEntity> _baseBusinessRules;

            public UpdateCommandHandler(IAsyncRepository<TEntity> asyncRepository, IMapper mapper, BaseBusinessRules<TEntity> baseBusinessRules)
            {
                _asyncRepository = asyncRepository;
                _mapper = mapper;
                _baseBusinessRules = baseBusinessRules;
            }

            public async Task<CustomResponseDto<TModel>> Handle(UpdateCommand<TEntity, TModel> request, CancellationToken cancellationToken)
            {
                await _baseBusinessRules.BaseIdShouldExistWhenSelected(request.Model.Id);
                TEntity? entity = await _asyncRepository.GetAsync(x => x.Id == request.Model.Id);

                _mapper.Map(request.Model, entity);
                TEntity updatedEntity = await _asyncRepository.UpdateAsync(entity);
                TModel updatedModel = _mapper.Map<TModel>(updatedEntity);

                return CustomResponseDto<TModel>.Success((int)HttpStatusCode.OK, updatedModel, isSuccess: true);
            }
        }
    }
}
