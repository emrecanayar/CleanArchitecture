using Core.Application.Pipelines.Authorization;
using Core.Domain.Entities;
using MediatR;
using rentACar.Application.Features.OperationClaims.Dtos;
using rentACar.Application.Features.OperationClaims.Rules;
using rentACar.Application.Services.Repositories;

namespace rentACar.Application.Features.OperationClaims.Commands.DeleteOperationClaim
{
    public class DeleteOperationClaimCommand : IRequest<DeletedOperationClaimDto>, ISecuredRequest
    {
        public int Id { get; set; }
        public string[] Roles => throw new NotImplementedException();

        public class DeleteOperationClaimCommandHanlder : IRequestHandler<DeleteOperationClaimCommand, DeletedOperationClaimDto>
        {
            private readonly IOperationClaimRepository _operationClaimRepository;
            private readonly OperationClaimBusinessRules _operationClaimBusinessRules;

            public DeleteOperationClaimCommandHanlder(IOperationClaimRepository operationClaimRepository, OperationClaimBusinessRules operationClaimBusinessRules)
            {
                _operationClaimRepository = operationClaimRepository;
                _operationClaimBusinessRules = operationClaimBusinessRules;
            }

            public async Task<DeletedOperationClaimDto> Handle(DeleteOperationClaimCommand request, CancellationToken cancellationToken)
            {
                await _operationClaimBusinessRules.OperationClaimIdShouldExistWhenSelected(request.Id);
                OperationClaim mappedOperationClaim = ObjectMapper.Mapper.Map<OperationClaim>(request);
                OperationClaim deletedOperationClaim = await _operationClaimRepository.DeleteAsync(mappedOperationClaim);
                DeletedOperationClaimDto deletedOperationClaimDto =
                    ObjectMapper.Mapper.Map<DeletedOperationClaimDto>(deletedOperationClaim);
                return deletedOperationClaimDto;
            }
        }
    }
}
