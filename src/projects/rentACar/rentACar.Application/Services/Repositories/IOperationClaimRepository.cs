using Core.Domain.Entities;
using Core.Persistence.Repositories;

namespace rentACar.Application.Services.Repositories
{
    public interface IOperationClaimRepository : IAsyncRepository<OperationClaim>, IRepository<OperationClaim>
    {
    }

}
