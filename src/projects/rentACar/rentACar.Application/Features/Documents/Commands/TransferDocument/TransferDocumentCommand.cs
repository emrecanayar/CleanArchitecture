using Core.Helpers.Helpers;
using MediatR;
using rentACar.Application.Features.Documents.Dtos;
using rentACar.Application.Features.Documents.Rules;
using rentACar.Application.Services.DocumentService;
using rentACar.Application.Services.Repositories;

namespace rentACar.Application.Features.Documents.Commands.TransferDocument
{
    public class TransferDocumentCommand : IRequest<TransferDocumentDto>
    {
        public string Token { get; set; }
        public string NewFolderPath { get; set; }
        public string WebRootPath { get; set; }

        public class TransferDocumentCommandHandler : IRequestHandler<TransferDocumentCommand, TransferDocumentDto>
        {
            private IDocumentService _documentService;
            private readonly DocumentBusinessRules _documentBusinessRules;

            public TransferDocumentCommandHandler(IDocumentRepository documentRepository, DocumentBusinessRules documentBusinessRules, IDocumentService documentService)
            {
                _documentBusinessRules = documentBusinessRules;
                _documentService = documentService;
            }

            public async Task<TransferDocumentDto> Handle(TransferDocumentCommand request, CancellationToken cancellationToken)
            {
                var result = await _documentService.TransferFile(request.Token, request.NewFolderPath, request.WebRootPath);

                var addedOrUpdatedResult = await _documentBusinessRules.AddOrUpdateDocument(new DocumentDto
                {
                    FileType = _documentBusinessRules.DetectFileType(result.Path),
                    DocumentName = "",
                    Path = result.Path,
                    Extension = FileInfoHelper.GetFileExtension(result.Path),
                    Key = Guid.NewGuid().ToString()
                });

                return new TransferDocumentDto { NewFolderPath = result.Path, Token = addedOrUpdatedResult.Token };
            }
        }
    }
}
