using MediatR;
using rentACar.Application.Features.Documents.Dtos;
using rentACar.Application.Features.Documents.Rules;
using rentACar.Application.Services.Repositories;
using rentACar.Domain.Entities;

namespace rentACar.Application.Features.Documents.Queries.GetByTokenDocument
{
    public class GetByTokenDocumentQuery : IRequest<DocumentDto>
    {
        public string Token { get; set; }

        public class GetByTokenDocumentQueryHandler : IRequestHandler<GetByTokenDocumentQuery, DocumentDto>
        {
            private readonly IDocumentRepository _documentRepository;
            private readonly DocumentBusinessRules _documentBusinessRules;

            public GetByTokenDocumentQueryHandler(IDocumentRepository documentRepository, DocumentBusinessRules documentBusinessRules)
            {
                _documentRepository = documentRepository;
                _documentBusinessRules = documentBusinessRules;
            }

            public async Task<DocumentDto> Handle(GetByTokenDocumentQuery request, CancellationToken cancellationToken)
            {
                Document? document = await _documentRepository.GetAsync(d => d.Token == request.Token);
                _documentBusinessRules.DocumentShouldExistWhenRequested(document);
                DocumentDto documentDto = ObjectMapper.Mapper.Map<DocumentDto>(document);
                return documentDto;

            }
        }
    }
}
