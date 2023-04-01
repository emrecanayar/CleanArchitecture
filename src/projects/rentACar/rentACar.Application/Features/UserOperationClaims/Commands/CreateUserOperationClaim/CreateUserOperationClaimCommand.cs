using Application.Features.UserOperationClaims.Dtos;
using Application.Features.UserOperationClaims.Rules;
using AutoMapper;
using Core.Application.Pipelines.Authorization;
using Core.Domain.Entities;
using MediatR;
using rentACar.Application.Features;
using rentACar.Application.Services.Repositories;
using static Application.Features.UserOperationClaims.Constants.OperationClaims;
using static Core.Domain.Constants.OperationClaims;

namespace Application.Features.UserOperationClaims.Commands.CreateUserOperationClaim;

public class CreateUserOperationClaimCommand : IRequest<CreatedUserOperationClaimDto>, ISecuredRequest
{
    public int UserId { get; set; }
    public int OperationClaimId { get; set; }

    public string[] Roles => new[] { Admin, UserOperationClaimAdd };

    public class
        CreateUserOperationClaimCommandHandler : IRequestHandler<CreateUserOperationClaimCommand,
            CreatedUserOperationClaimDto>
    {
        private readonly IUserOperationClaimRepository _userOperationClaimRepository;
        private readonly IMapper _mapper;
        private readonly UserOperationClaimBusinessRules _userOperationClaimBusinessRules;

        public CreateUserOperationClaimCommandHandler(IUserOperationClaimRepository userOperationClaimRepository,
                                                      UserOperationClaimBusinessRules userOperationClaimBusinessRules)
        {
            _userOperationClaimRepository = userOperationClaimRepository;
            _userOperationClaimBusinessRules = userOperationClaimBusinessRules;
        }

        public async Task<CreatedUserOperationClaimDto> Handle(CreateUserOperationClaimCommand request,
                                                               CancellationToken cancellationToken)
        {
            UserOperationClaim mappedUserOperationClaim = ObjectMapper.Mapper.Map<UserOperationClaim>(request);
            UserOperationClaim createdUserOperationClaim =
                await _userOperationClaimRepository.AddAsync(mappedUserOperationClaim);
            CreatedUserOperationClaimDto createdUserOperationClaimDto =
                ObjectMapper.Mapper.Map<CreatedUserOperationClaimDto>(createdUserOperationClaim);
            return createdUserOperationClaimDto;
        }
    }
}