using AutoMapper;
using Core.Domain.Entities;
using MediatR;
using rentACar.Application.Features.OperationClaims.Dtos;
using rentACar.Application.Features.OperationClaims.Rules;
using rentACar.Application.Services.Repositories;

namespace rentACar.Application.Features.OperationClaims.Queries.GetByIdOperationClaim
{
    public class GetByIdOperationClaimQuery : IRequest<OperationClaimDto>
    {
        public int Id { get; set; }

        public class GetByIdOperationClaimQueryHandler : IRequestHandler<GetByIdOperationClaimQuery, OperationClaimDto>
        {
            private readonly IOperationClaimRepository _operationClaimRepository;
            private readonly IMapper _mapper;
            private readonly OperationClaimBusinessRules _operationClaimBusinessRules;

            public GetByIdOperationClaimQueryHandler(IOperationClaimRepository operationClaimRepository,
                                                     OperationClaimBusinessRules operationClaimBusinessRules)
            {
                _operationClaimRepository = operationClaimRepository;
                _operationClaimBusinessRules = operationClaimBusinessRules;
            }


            public async Task<OperationClaimDto> Handle(GetByIdOperationClaimQuery request,
                                                        CancellationToken cancellationToken)
            {
                await _operationClaimBusinessRules.OperationClaimIdShouldExistWhenSelected(request.Id);

                OperationClaim? operationClaim = await _operationClaimRepository.GetAsync(b => b.Id == request.Id);
                OperationClaimDto operationClaimDto = ObjectMapper.Mapper.Map<OperationClaimDto>(operationClaim);
                return operationClaimDto;
            }
        }
    }
}
