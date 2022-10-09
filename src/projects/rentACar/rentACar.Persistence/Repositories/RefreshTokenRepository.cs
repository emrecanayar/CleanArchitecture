using Core.Persistence.Repositories;
using Core.Security.Entities;
using rentACar.Application.Services.Repositories;
using rentACar.Persistence.Contexts;

namespace rentACar.Persistence.Repositories
{
    public class RefreshTokenRepository : EfRepositoryBase<RefreshToken, BaseDbContext>, IRefreshTokenRepository
    {
        public RefreshTokenRepository(BaseDbContext context) : base(context)
        {
        }
    }
}
