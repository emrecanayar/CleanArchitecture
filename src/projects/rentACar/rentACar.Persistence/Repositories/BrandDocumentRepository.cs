using Core.Persistence.Repositories;
using rentACar.Application.Services.Repositories;
using rentACar.Domain.Entities;
using rentACar.Persistence.Contexts;

namespace rentACar.Persistence.Repositories
{
    public class BrandDocumentRepository : EfRepositoryBase<BrandDocument, BaseDbContext>, IBrandDocumentRepository
    {
        private readonly string FILE_FOLDER = Path.Combine("Resources", "Files", "BrandDocuments");
        private readonly IDocumentRepository _documentRepository;
        public BrandDocumentRepository(BaseDbContext context, IDocumentRepository documentRepository) : base(context)
        {
            _documentRepository = documentRepository;
        }

        public async Task<bool> UpsertWithFileTransfer(List<string> documentTokens, int brandId, string webRootPath)
        {
            foreach (string documentToken in documentTokens)
            {
                var document = await _documentRepository.GetAsync(x => x.Token == documentToken);
                if (document == null) continue;

                var isHasBrandDocument = await GetAsync(x => x.BrandId == brandId && x.DocumentId == document.Id);

                if (isHasBrandDocument == null)
                {
                    var brandDocument = new BrandDocument
                    {
                        BrandId = brandId,
                        DocumentId = document.Id
                    };

                    var newBrandDocument = await AddAsync(brandDocument);
                    await _documentRepository.TransferFile(documentToken, FILE_FOLDER, webRootPath);
                }
                else
                {
                    isHasBrandDocument.BrandId = brandId;
                    isHasBrandDocument.DocumentId = document.Id;
                    var updateCompanyDocument = await UpdateAsync(isHasBrandDocument);

                }
            }

            return true;
        }
    }
}
