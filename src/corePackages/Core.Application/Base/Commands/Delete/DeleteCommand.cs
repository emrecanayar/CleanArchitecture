using Core.Application.Base.Rules;
using Core.Application.ResponseTypes.Concrete;
using Core.Domain.Entities.Base;
using Core.Persistence.Repositories;
using MediatR;
using System.Net;

namespace Core.Application.Base.Commands.Delete
{
    public class DeleteCommand<TEntity, TModel> : IRequest<CustomResponseDto<bool>>
       where TEntity : Entity
       where TModel : IEntityModel
    {
        public TModel Model { get; set; }

        public DeleteCommand(TModel model)
        {
            Model = model;
        }

        public class DeleteCommandHandler : IRequestHandler<DeleteCommand<TEntity, TModel>, CustomResponseDto<bool>>
        {
            private readonly IAsyncRepository<TEntity> _asyncRepository;
            private readonly BaseBusinessRules<TEntity> _baseBusinessRules;

            public DeleteCommandHandler(IAsyncRepository<TEntity> asyncRepository, BaseBusinessRules<TEntity> baseBusinessRules)
            {
                _asyncRepository = asyncRepository;
                _baseBusinessRules = baseBusinessRules;
            }

            public async Task<CustomResponseDto<bool>> Handle(DeleteCommand<TEntity, TModel> request, CancellationToken cancellationToken)
            {
                await _baseBusinessRules.BaseIdShouldExistWhenSelected(request.Model.Id);
                TEntity? entity = await _asyncRepository.GetAsync(x => x.Id == request.Model.Id);
                await _asyncRepository.RemoveAsync(entity);

                return CustomResponseDto<bool>.Success((int)HttpStatusCode.OK, true, isSuccess: true);
            }
        }
    }

}
