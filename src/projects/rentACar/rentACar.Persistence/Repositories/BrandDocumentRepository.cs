using Core.Persistence.Repositories;
using rentACar.Application.Services.Repositories;
using rentACar.Domain.Entities;
using rentACar.Persistence.Contexts;

namespace rentACar.Persistence.Repositories
{
    public class BrandDocumentRepository : EfRepositoryBase<BrandDocument, BaseDbContext>, IBrandDocumentRepository
    {
        public BrandDocumentRepository(BaseDbContext context) : base(context)
        {

        }


    }
}
