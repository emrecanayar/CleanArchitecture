using Core.Persistence.Repositories;
using rentACar.Domain.Entities;

namespace rentACar.Application.Services.Repositories
{
    public interface IBrandDocumentRepository : IAsyncRepository<BrandDocument>, IRepository<BrandDocument>
    {
        Task<bool> UpsertWithFileTransfer(List<string> documentTokens, int brandId, string webRootPath);
    }
}
