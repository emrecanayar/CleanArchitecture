using Core.Domain.Entities;
using Core.Persistence.Repositories;
using rentACar.Application.Services.Repositories;
using rentACar.Persistence.Contexts;

namespace rentACar.Persistence.Repositories
{
    public class EmailAuthenticatorRepository : EfRepositoryBase<EmailAuthenticator, BaseDbContext>, IEmailAuthenticatorRepository
    {
        public EmailAuthenticatorRepository(BaseDbContext context) : base(context)
        {
        }
    }
}
