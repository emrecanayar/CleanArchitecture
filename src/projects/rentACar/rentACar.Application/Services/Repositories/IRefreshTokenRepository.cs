using Core.Domain.Entities;
using Core.Persistence.Repositories;

namespace rentACar.Application.Services.Repositories
{
    public interface IRefreshTokenRepository : IAsyncRepository<RefreshToken>, IRepository<RefreshToken>
    {
    }

}
