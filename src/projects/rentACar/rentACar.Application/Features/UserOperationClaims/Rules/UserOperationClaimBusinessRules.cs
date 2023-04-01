using Application.Features.UserOperationClaims.Constants;
using Core.Application.Rules;
using Core.CrossCuttingConcerns.Exceptions;
using Core.Domain.Entities;
using rentACar.Application.Services.Repositories;

namespace Application.Features.UserOperationClaims.Rules;

public class UserOperationClaimBusinessRules : BaseBusinessRules
{
    private readonly IUserOperationClaimRepository _userOperationClaimRepository;

    public UserOperationClaimBusinessRules(IUserOperationClaimRepository userOperationClaimRepository)
    {
        _userOperationClaimRepository = userOperationClaimRepository;
    }

    public async Task UserOperationClaimIdShouldExistWhenSelected(int id)
    {
        UserOperationClaim? result = await _userOperationClaimRepository.GetAsync(b => b.Id == id);
        if (result == null) throw new BusinessException(UserOperationClaimMessages.UserOperationClaimNotExists);
    }
}