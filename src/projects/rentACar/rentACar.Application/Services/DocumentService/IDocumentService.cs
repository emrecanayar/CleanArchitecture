using rentACar.Application.Features.Documents.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace rentACar.Application.Services.DocumentService
{
    public interface IDocumentService
    {
        Task<FileUploadResultDto> TransferFile(string token, string newFolderPath, string webRootPath);
    }
}
