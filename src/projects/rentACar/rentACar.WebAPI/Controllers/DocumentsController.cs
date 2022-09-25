using Microsoft.AspNetCore.Mvc;
using rentACar.Application.Features.Brands.Commands.CreateBrand;
using rentACar.Application.Features.Brands.Dtos;
using rentACar.Application.Features.Documents.Commands.CreateDocument;
using rentACar.Application.Features.Documents.Dtos;
using rentACar.WebAPI.Controllers.Base;

namespace rentACar.WebAPI.Controllers
{
    public class DocumentsController : BaseController
    {
        private readonly IWebHostEnvironment _environment;

        public DocumentsController(IWebHostEnvironment environment)
        {
            _environment = environment;
        }

        [HttpPost]
        public async Task<IActionResult> Add(IFormFile file)
        {
            CreatedDocumentDto result = await Mediator.Send(new CreateDocumentCommand { File = file, WebRootPath = _environment.WebRootPath });
            return Created("", result);
        }
    }
}
