using Core.Persistence.Repositories;
using rentACar.Application.Features.Documents.Dtos;
using rentACar.Domain.Entities;

namespace rentACar.Application.Services.Repositories
{
    public interface IDocumentRepository : IAsyncRepository<Document>, IRepository<Document>
    {
        Task<FileUploadResultDto> TransferFile(string token, string newFolderPath, string webRootPath);
    }
}
