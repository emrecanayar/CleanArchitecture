using Core.Helpers.Helpers;
using Core.Persistence.Repositories;
using Core.Security.Constants;
using Core.Security.Hashing;
using rentACar.Application.Features.Documents.Dtos;
using rentACar.Application.Services.Repositories;
using rentACar.Domain.Entities;
using rentACar.Persistence.Contexts;
using System.Text.Json;

namespace rentACar.Persistence.Repositories
{
    public class DocumentRepository : EfRepositoryBase<Document, BaseDbContext>, IDocumentRepository
    {
        public DocumentRepository(BaseDbContext context) : base(context)
        {
        }

   
    }
}
