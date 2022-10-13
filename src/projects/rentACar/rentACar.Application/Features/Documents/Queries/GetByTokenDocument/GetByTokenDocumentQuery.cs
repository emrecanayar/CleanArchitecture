using Core.Application.ResponseTypes.Concrete;
using MediatR;
using rentACar.Application.Features.Documents.Dtos;
using rentACar.Application.Features.Documents.Rules;
using rentACar.Application.Services.Repositories;
using rentACar.Domain.Entities;
using System.Net;

namespace rentACar.Application.Features.Documents.Queries.GetByTokenDocument
{
    public class GetByTokenDocumentQuery : IRequest<CustomResponseDto<DocumentDto>>
    {
        public string Token { get; set; }

        public class GetByTokenDocumentQueryHandler : IRequestHandler<GetByTokenDocumentQuery, CustomResponseDto<DocumentDto>>
        {
            private readonly IDocumentRepository _documentRepository;
            private readonly DocumentBusinessRules _documentBusinessRules;

            public GetByTokenDocumentQueryHandler(IDocumentRepository documentRepository, DocumentBusinessRules documentBusinessRules)
            {
                _documentRepository = documentRepository;
                _documentBusinessRules = documentBusinessRules;
            }

            public async Task<CustomResponseDto<DocumentDto>> Handle(GetByTokenDocumentQuery request, CancellationToken cancellationToken)
            {
                Document? document = await _documentRepository.GetAsync(d => d.Token == request.Token);
                _documentBusinessRules.DocumentShouldExistWhenRequested(document);
                DocumentDto documentDto = ObjectMapper.Mapper.Map<DocumentDto>(document);
                return CustomResponseDto<DocumentDto>.Success((int)HttpStatusCode.OK, documentDto, isSuccess: true);

            }
        }
    }
}
