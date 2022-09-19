using Core.CrossCuttingConcerns.Exceptions;
using Core.Persistence.Paging;
using rentACar.Application.Services.Repositories;
using rentACar.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace rentACar.Application.Features.Documents.Rules
{
    public class DocumentBusinessRules
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
    }
}
