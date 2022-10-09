using Core.Persistence.Repositories;
using Core.Security.Entities;

namespace rentACar.Application.Services.Repositories
{
    public interface IUserRepository : IAsyncRepository<User>, IRepository<User>
    {
    }

}
