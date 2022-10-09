using Core.Persistence.Repositories;
using Core.Security.Entities;

namespace rentACar.Application.Services.Repositories
{
    public interface IRefreshTokenRepository : IAsyncRepository<RefreshToken>, IRepository<RefreshToken>
    {
    }

}
