using Core.Persistence.Repositories;
using rentACar.Application.Services.Repositories;
using rentACar.Domain.Entities;
using rentACar.Persistence.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace rentACar.Persistence.Repositories
{
    public class DocumentRepository : EfRepositoryBase<Document, BaseDbContext>, IDocumentRepository
    {
        public DocumentRepository(BaseDbContext context) : base(context)
        {
        }
    }
}
