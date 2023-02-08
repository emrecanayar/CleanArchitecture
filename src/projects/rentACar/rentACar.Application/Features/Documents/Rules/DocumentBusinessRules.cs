using Core.Application.Rules;
using Core.CrossCuttingConcerns.Exceptions;
using Core.Domain.ComplexTypes;
using Core.Helpers.Helpers;
using Core.Persistence.Paging;
using Core.Security.Constants;
using rentACar.Application.Features.Documents.Dtos;
using rentACar.Application.Services.Repositories;
using rentACar.Domain.Entities;
using System.Text.Json;

namespace rentACar.Application.Features.Documents.Rules
{
    public class DocumentBusinessRules : BaseBusinessRules
    {
        private readonly IDocumentRepository _documentRepository;

        public DocumentBusinessRules(IDocumentRepository documentRepository)
        {
            _documentRepository = documentRepository;
        }

        public async Task DocumentTokenCanNotBeDuplicatedWhenInserted(string token)
        {
            IPaginate<Document> result = await _documentRepository.GetListAsync(d => d.Token == token);
            if (result.Items.Any()) throw new BusinessException("Document token exists");
        }

        public void DocumentShouldExistWhenRequested(Document document)
        {
            if (document == null) throw new BusinessException("Requested document does not exist");
        }

        public async Task<Document> AddOrUpdateDocument(DocumentDto documentDto)
        {
            var documentData = JsonSerializer.Serialize(documentDto);
            var encryptedPath = Core.Helpers.Helpers.HashingHelper.AESEncrypt(documentData, SecurityKeyConstant.DOCUMENT_SECURITY_KEY);
            var document = new Document
            {
                Id = documentDto.Id,
                Token = encryptedPath

            };
            await DocumentTokenCanNotBeDuplicatedWhenInserted(document.Token);

            if (document.Id > 0)
                await _documentRepository.UpdateAsync(document);
            else
                await _documentRepository.AddAsync(document);


            return document;
        }
        public FileType DetectFileType(string filePath)
        {
            var extension = FileInfoHelper.GetFileExtension(filePath);
            switch (extension)
            {
                case ".xlsx":
                case ".xls":
                case ".xlsm":
                case ".xlm":
                    return FileType.Xls;
                case ".docx":
                case ".doc":
                    return FileType.Doc;
                case ".potx":
                case ".pot":
                case ".ppsx":
                case ".pps":
                case ".pptx":
                case ".ppt":
                    return FileType.Pps;
                case ".pdf":
                    return FileType.Pdf;
                case ".jpeg":
                case ".png":
                case ".jpg":
                case ".svg":
                    return FileType.Img;
                case ".mp4":
                    return FileType.Mp4;
                default:
                    return default(FileType);
            }
        }
    }
}
