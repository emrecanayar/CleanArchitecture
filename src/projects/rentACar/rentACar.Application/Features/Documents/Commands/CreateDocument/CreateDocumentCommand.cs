using Core.Helpers.Helpers;
using MediatR;
using Microsoft.AspNetCore.Http;
using rentACar.Application.Features.Documents.Dtos;
using rentACar.Application.Features.Documents.Rules;


namespace rentACar.Application.Features.Documents.Commands.CreateDocument
{
    public class CreateDocumentCommand : IRequest<CreatedDocumentDto>
    {
        public IFormFile File { get; set; }
        public string WebRootPath { get; set; }

        public class CreateDocumentCommandHandler : IRequestHandler<CreateDocumentCommand, CreatedDocumentDto>
        {
            private readonly string DOCUMENT_FOLDER = Path.Combine("Resources", "Files", "DocumentPool");
            private readonly DocumentBusinessRules _documentBusinessRules;

            public CreateDocumentCommandHandler(DocumentBusinessRules documentBusinessRules)
            {
                _documentBusinessRules = documentBusinessRules;
            }

            public async Task<CreatedDocumentDto> Handle(CreateDocumentCommand request, CancellationToken cancellationToken)
            {
                string filePath = FileHelper.GenerateURLForFile(request.File, request.WebRootPath, DOCUMENT_FOLDER);
                var document = await _documentBusinessRules.AddOrUpdateDocument(new DocumentDto
                {
                    FileType = _documentBusinessRules.DetectFileType(filePath),
                    DocumentName = request.File.FileName,
                    Path = filePath,
                    Extension = FileInfoHelper.GetFileExtension(filePath),
                    Key = Guid.NewGuid().ToString()
                });
                FileHelper.Upload(request.File, request.WebRootPath, filePath);
                return new CreatedDocumentDto { Path = filePath, Token = document.Token };
            }
        }
    }
}
