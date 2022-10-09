using Core.Persistence.Repositories;
using Core.Security.Entities;

namespace rentACar.Application.Services.Repositories
{
    public interface IUserOperationClaimRepository : IAsyncRepository<UserOperationClaim>, IRepository<UserOperationClaim>
    {
    }

}
