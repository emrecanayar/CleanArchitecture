using rentACar.Application.Features.Documents.Dtos;

namespace rentACar.Application.Services.DocumentService
{
    public interface IDocumentService
    {
        Task<FileUploadResultDto> TransferFile(string token, string newFolderPath, string webRootPath);
    }
}
