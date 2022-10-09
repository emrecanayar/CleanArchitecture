using Microsoft.AspNetCore.Mvc;
using rentACar.Application.Features.BrandDocuments.Commands.CreateBrandDocument;
using rentACar.WebAPI.Controllers.Base;

namespace rentACar.WebAPI.Controllers
{
    public class BrandDocumentsController : BaseController
    {
        private readonly IWebHostEnvironment _environment;

        public BrandDocumentsController(IWebHostEnvironment environment)
        {
            _environment = environment;
        }

        [HttpPost]
        public async Task<IActionResult> Add([FromBody] CreateBrandDocumentCommand createBrandDocumentCommand)
        {
            createBrandDocumentCommand.WebRootPath = _environment.WebRootPath;
            bool result = await Mediator.Send(createBrandDocumentCommand);
            return Created("", result);
        }
    }
}
