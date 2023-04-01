using Core.Domain.Entities;
using Core.Persistence.Repositories;

namespace rentACar.Application.Services.Repositories
{
    public interface IUserRepository : IAsyncRepository<User>, IRepository<User>
    {
    }

}
