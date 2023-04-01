using Core.Application.Requests;
using Core.Domain.Entities;
using Core.Persistence.Paging;
using MediatR;
using rentACar.Application.Features.OperationClaims.Models;
using rentACar.Application.Services.Repositories;

namespace rentACar.Application.Features.OperationClaims.Queries.GetListOperationClaim
{
    public class GetListOperationClaimQuery : IRequest<OperationClaimListModel>
    {
        public PageRequest PageRequest { get; set; }

        public class
            GetListOperationClaimQueryHandler : IRequestHandler<GetListOperationClaimQuery, OperationClaimListModel>
        {
            private readonly IOperationClaimRepository _operationClaimRepository;

            public GetListOperationClaimQueryHandler(IOperationClaimRepository operationClaimRepository)
            {
                _operationClaimRepository = operationClaimRepository;
            }

            public async Task<OperationClaimListModel> Handle(GetListOperationClaimQuery request,
                                                              CancellationToken cancellationToken)
            {
                IPaginate<OperationClaim> operationClaims = await _operationClaimRepository.GetListAsync(
                                                                index: request.PageRequest.Page,
                                                                size: request.PageRequest.PageSize);
                OperationClaimListModel mappedOperationClaimListModel =
                    ObjectMapper.Mapper.Map<OperationClaimListModel>(operationClaims);
                return mappedOperationClaimListModel;
            }
        }
    }
}
