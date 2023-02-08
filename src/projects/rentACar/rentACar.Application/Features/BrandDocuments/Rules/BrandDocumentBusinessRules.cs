using Core.Application.Rules;
using rentACar.Application.Services.DocumentService;
using rentACar.Application.Services.Repositories;
using rentACar.Domain.Entities;

namespace rentACar.Application.Features.BrandDocuments.Rules
{
    public class BrandDocumentBusinessRules : BaseBusinessRules
    {
        private readonly string FILE_FOLDER = Path.Combine("Resources", "Files", "BrandDocuments");
        private readonly IBrandDocumentRepository _brandDocumentRepository;
        private readonly IDocumentService _documentService;
        private readonly IDocumentRepository _documentRepository;
        public BrandDocumentBusinessRules(IBrandDocumentRepository brandDocumentRepository, IDocumentRepository documentRepository, IDocumentService documentService)
        {
            _brandDocumentRepository = brandDocumentRepository;
            _documentService = documentService;
            _documentRepository = documentRepository;
        }

        public async Task<bool> UpsertWithFileTransfer(List<string> documentTokens, int brandId, string webRootPath)
        {
            foreach (string documentToken in documentTokens)
            {
                var document = await _documentRepository.GetAsync(x => x.Token == documentToken);
                if (document == null) continue;

                var isHasBrandDocument = await _brandDocumentRepository.GetAsync(x => x.BrandId == brandId && x.DocumentId == document.Id);

                if (isHasBrandDocument == null)
                {
                    var brandDocument = new BrandDocument
                    {
                        BrandId = brandId,
                        DocumentId = document.Id
                    };

                    var newBrandDocument = await _brandDocumentRepository.AddAsync(brandDocument);
                    await _documentService.TransferFile(documentToken, FILE_FOLDER, webRootPath);
                }
                else
                {
                    isHasBrandDocument.BrandId = brandId;
                    isHasBrandDocument.DocumentId = document.Id;
                    var updateCompanyDocument = await _brandDocumentRepository.UpdateAsync(isHasBrandDocument);

                }
            }

            return true;
        }

    }
}
