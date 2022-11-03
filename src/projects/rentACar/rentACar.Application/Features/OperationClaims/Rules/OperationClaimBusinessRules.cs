using Core.Application.Rules;
using Core.CrossCuttingConcerns.Exceptions;
using Core.Security.Entities;
using rentACar.Application.Features.OperationClaims.Constants;
using rentACar.Application.Services.Repositories;

namespace rentACar.Application.Features.OperationClaims.Rules
{
    public class OperationClaimBusinessRules : BaseBusinessRules
    {
        private readonly IOperationClaimRepository _operationClaimRepository;

        public OperationClaimBusinessRules(IOperationClaimRepository operationClaimRepository)
        {
            _operationClaimRepository = operationClaimRepository;
        }

        public async Task OperationClaimIdShouldExistWhenSelected(int id)
        {
            OperationClaim? operationClaim = await _operationClaimRepository.GetAsync(x => x.Id == id);
            if (operationClaim is null) throw new BusinessException(OperationClaimMessages.OperationClaimNotExists);
        }
    }
}
