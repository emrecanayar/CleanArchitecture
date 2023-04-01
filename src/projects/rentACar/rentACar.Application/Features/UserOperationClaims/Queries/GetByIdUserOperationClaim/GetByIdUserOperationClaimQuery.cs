using Application.Features.UserOperationClaims.Dtos;
using Application.Features.UserOperationClaims.Rules;
using Core.Domain.Entities;
using MediatR;
using rentACar.Application.Features;
using rentACar.Application.Services.Repositories;

namespace Application.Features.UserOperationClaims.Queries.GetByIdUserOperationClaim;

public class GetByIdUserOperationClaimQuery : IRequest<UserOperationClaimDto>
{
    public int Id { get; set; }

    public class
        GetByIdUserOperationClaimQueryHandler : IRequestHandler<GetByIdUserOperationClaimQuery, UserOperationClaimDto>
    {
        private readonly IUserOperationClaimRepository _userOperationClaimRepository;
        private readonly UserOperationClaimBusinessRules _userOperationClaimBusinessRules;

        public GetByIdUserOperationClaimQueryHandler(IUserOperationClaimRepository userOperationClaimRepository,
                                                     UserOperationClaimBusinessRules userOperationClaimBusinessRules)
        {
            _userOperationClaimRepository = userOperationClaimRepository;
            _userOperationClaimBusinessRules = userOperationClaimBusinessRules;
        }


        public async Task<UserOperationClaimDto> Handle(GetByIdUserOperationClaimQuery request,
                                                        CancellationToken cancellationToken)
        {
            await _userOperationClaimBusinessRules.UserOperationClaimIdShouldExistWhenSelected(request.Id);

            UserOperationClaim? userOperationClaim =
                await _userOperationClaimRepository.GetAsync(b => b.Id == request.Id);
            UserOperationClaimDto userOperationClaimDto = ObjectMapper.Mapper.Map<UserOperationClaimDto>(userOperationClaim);
            return userOperationClaimDto;
        }
    }
}