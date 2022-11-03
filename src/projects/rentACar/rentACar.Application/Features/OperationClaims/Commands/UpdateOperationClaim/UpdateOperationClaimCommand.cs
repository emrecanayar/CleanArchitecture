using Core.Application.Pipelines.Authorization;
using Core.Security.Entities;
using MediatR;
using rentACar.Application.Features.OperationClaims.Dtos;
using rentACar.Application.Features.OperationClaims.Rules;
using rentACar.Application.Services.Repositories;
using static rentACar.Application.Features.OperationClaims.Constants.OperationClaims;
using static rentACar.Domain.Constants.OperationClaims;

namespace rentACar.Application.Features.OperationClaims.Commands.UpdateOperationClaim
{
    public class UpdateOperationClaimCommand : IRequest<UpdatedOperationClaimDto>, ISecuredRequest
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string[] Roles => new[] { Admin, OperationClaimUpdate };

        public class UpdateOperationClaimCommandHandler : IRequestHandler<UpdateOperationClaimCommand, UpdatedOperationClaimDto>
        {
            private readonly IOperationClaimRepository _operationClaimRepository;
            private readonly OperationClaimBusinessRules _operationClaimBusinessRules;

            public UpdateOperationClaimCommandHandler(IOperationClaimRepository operationClaimRepository, OperationClaimBusinessRules operationClaimBusinessRules)
            {
                _operationClaimRepository = operationClaimRepository;
                _operationClaimBusinessRules = operationClaimBusinessRules;
            }

            public async Task<UpdatedOperationClaimDto> Handle(UpdateOperationClaimCommand request, CancellationToken cancellationToken)
            {
                OperationClaim mappedOperationClaim = ObjectMapper.Mapper.Map<OperationClaim>(request);
                OperationClaim updatedOperationClaim = await _operationClaimRepository.UpdateAsync(mappedOperationClaim);
                UpdatedOperationClaimDto updatedOperationClaimDto = ObjectMapper.Mapper.Map<UpdatedOperationClaimDto>(updatedOperationClaim);

                return updatedOperationClaimDto;
            }
        }
    }
}
