using Core.Persistence.Repositories;
using rentACar.Application.Services.Repositories;
using rentACar.Domain.Entities;
using rentACar.Persistence.Contexts;

namespace rentACar.Persistence.Repositories
{
    public class BrandRepository : EfRepositoryBase<Brand, BaseDbContext>, IBrandRepository
    {
        public BrandRepository(BaseDbContext context) : base(context)
        {
        }
        
    }
}
