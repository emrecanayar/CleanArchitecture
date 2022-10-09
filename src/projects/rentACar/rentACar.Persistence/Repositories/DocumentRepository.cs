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

        public async Task<FileUploadResultDto> TransferFile(string token, string newFolderPath, string webRootPath)
        {
            var document = await GetAsync(x => x.Token == token);
            if (document == null) return null;

            var decryptedDocumentData = HashingHelper.AESDecrypt(document.Token, SecurityKeyConstant.DOCUMENT_SECURITY_KEY);

            var documentDto = JsonSerializer.Deserialize<DocumentDto>(decryptedDocumentData);

            var newLocationFullPath = DocumentTransferNewLocation(documentDto.Path, newFolderPath, webRootPath);

            documentDto.Path = FileHelper.GetURLForFileFromFullPath(webRootPath, newLocationFullPath);

            return new FileUploadResultDto { Id = documentDto.Id, Path = documentDto.Path };
        }

        private string DocumentTransferNewLocation(string fileFullPath, string newFolder, string webRootPath)
        {
            var fileName = Path.GetFileName(fileFullPath);
            var oldLocation = Path.Combine(webRootPath, fileFullPath.Replace("/", "\\"));
            var newLocation = FileHelper.GetNewPath(webRootPath, newFolder, fileName);
            if (File.Exists(newLocation))
            {
                var extension = Path.GetExtension(fileName);
                fileName = Guid.NewGuid().ToString() + extension;
                newLocation = Path.Combine(webRootPath, newFolder, fileName);
            }
            File.Move(oldLocation, newLocation, false);
            return newLocation;
        }
    }
}
