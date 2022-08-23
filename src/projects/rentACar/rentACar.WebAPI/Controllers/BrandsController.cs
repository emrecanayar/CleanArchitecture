using Microsoft.AspNetCore.Mvc;
using rentACar.Application.Features.Brands.Commands.CreateBrand;
using rentACar.Application.Features.Brands.Dtos;
using rentACar.WebAPI.Controllers.Base;

namespace rentACar.WebAPI.Controllers
{
    public class BrandsController : BaseController
    {
        [HttpPost]
        public async Task<IActionResult> Add([FromBody] CreateBrandCommand createBrandCommand)
        {
            CreatedBrandDto result = await Mediator.Send(createBrandCommand);
            return Created("", result);
        }

    }
}
