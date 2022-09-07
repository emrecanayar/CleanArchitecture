using Core.Application.Requests;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using rentACar.Application.Features.Models.Models;
using rentACar.Application.Features.Models.Queries.GetListModelPaginate;
using rentACar.WebAPI.Controllers.Base;

namespace rentACar.WebAPI.Controllers;

public class ModelsController : BaseController
{
    [HttpGet]
    public async Task<ActionResult> GetList([FromQuery] PageRequest pageRequest)
    {
        GetListModelPaginateQuery getListModelPaginateQuery = new GetListModelPaginateQuery { PageRequest = pageRequest };
        ModelListModel result = await Mediator.Send(getListModelPaginateQuery);
        return Ok(result);
    }
}
