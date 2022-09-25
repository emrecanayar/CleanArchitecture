using Core.CrossCuttingConcerns.Exceptions;
using Core.Helpers.Helpers;
using Core.Persistence.ComplexTypes;
using Core.Persistence.Paging;
using Core.Security.Hashing;
using Microsoft.IdentityModel.Tokens;
using rentACar.Application.Features.Documents.Dtos;
using rentACar.Application.Services.Repositories;
using rentACar.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace rentACar.Application.Features.Documents.Rules
{
    public class DocumentBusinessRules
    {
        private readonly string securitykey = "zKbVE-yEO'Gg9n7)e[8vJOf=dsUf&eP}";
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

        public async Task<Document> AddDocument(DocumentDto documentDto)
        {
            var documentData = JsonSerializer.Serialize(documentDto);
            var encryptedPath = HashingHelper.AESEncrypt(documentData, securitykey);
            var document = new Document
            {
                Id = documentDto.Id,
                Token = encryptedPath

            };
            await DocumentTokenCanNotBeDuplicatedWhenInserted(document.Token);
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
