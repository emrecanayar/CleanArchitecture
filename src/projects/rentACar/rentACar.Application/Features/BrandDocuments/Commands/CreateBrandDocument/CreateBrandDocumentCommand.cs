using MediatR;
using Newtonsoft.Json;
using rentACar.Application.Features.BrandDocuments.Rules;

namespace rentACar.Application.Features.BrandDocuments.Commands.CreateBrandDocument
{
    public class CreateBrandDocumentCommand : IRequest<bool>
    {
        public List<string> DocumentTokens { get; set; }
        public int BrandId { get; set; }
        [JsonIgnore]
        public string WebRootPath { get; set; }


        public class CreateBrandDocumentCommandHandler : IRequestHandler<CreateBrandDocumentCommand, bool>
        {
            private readonly BrandDocumentBusinessRules _documentBusinessRules;

            public CreateBrandDocumentCommandHandler(BrandDocumentBusinessRules documentBusinessRules)
            {
                _documentBusinessRules = documentBusinessRules;
            }

            public async Task<bool> Handle(CreateBrandDocumentCommand request, CancellationToken cancellationToken)
            {
                var brandDocumentResult = await _documentBusinessRules.UpsertWithFileTransfer(request.DocumentTokens, request.BrandId, request.WebRootPath);

                return brandDocumentResult;
            }
        }
    }
}
