using Core.Persistence.Repositories;
using rentACar.Domain.Entities;

namespace rentACar.Application.Services.Repositories
{
    public interface IDocumentRepository : IAsyncRepository<Document>, IRepository<Document>
    {

    }
}
