using Core.Application.Pipelines.Authorization;
using Core.Domain.Entities;
using MediatR;
using rentACar.Application.Features.OperationClaims.Dtos;
using rentACar.Application.Features.OperationClaims.Rules;
using rentACar.Application.Services.Repositories;
using static Core.Domain.Constants.OperationClaims;
using static rentACar.Application.Features.OperationClaims.Constants.OperationClaims;
namespace rentACar.Application.Features.OperationClaims.Commands.CreateOperationClaim
{
    public class CreateOperationClaimCommand : IRequest<CreatedOperationClaimDto>, ISecuredRequest
    {
        public string Name { get; set; }
        public string[] Roles => new[] { Admin, OperationClaimAdd };

        public class CreateOperationClaimCommandHanlder : IRequestHandler<CreateOperationClaimCommand, CreatedOperationClaimDto>
        {
            private readonly IOperationClaimRepository _operationClaimRepository;
            private readonly OperationClaimBusinessRules _operationClaimBusinessRules;

            public CreateOperationClaimCommandHanlder(IOperationClaimRepository operationClaimRepository, OperationClaimBusinessRules operationClaimBusinessRules)
            {
                _operationClaimRepository = operationClaimRepository;
                _operationClaimBusinessRules = operationClaimBusinessRules;
            }

            public async Task<CreatedOperationClaimDto> Handle(CreateOperationClaimCommand request, CancellationToken cancellationToken)
            {
                OperationClaim mappedOperationClaim = ObjectMapper.Mapper.Map<OperationClaim>(request);
                OperationClaim createdOperationClaim = await _operationClaimRepository.AddAsync(mappedOperationClaim);
                CreatedOperationClaimDto createdOperationClaimDto = ObjectMapper.Mapper.Map<CreatedOperationClaimDto>(createdOperationClaim);
                return createdOperationClaimDto;
            }
        }
    }
}
