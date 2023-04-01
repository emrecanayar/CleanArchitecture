using Core.Domain.Entities;
using Core.Persistence.Repositories;
using rentACar.Application.Services.Repositories;
using rentACar.Persistence.Contexts;

namespace rentACar.Persistence.Repositories
{
    public class UserRepository : EfRepositoryBase<User, BaseDbContext>, IUserRepository
    {
        public UserRepository(BaseDbContext context) : base(context)
        {
        }
    }
}
