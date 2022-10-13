using Core.Application.Requests;
using Core.Application.ResponseTypes.Concrete;
using Core.Persistence.Dynamic;
using Microsoft.AspNetCore.Mvc;
using rentACar.Application.Features.Models.Models;
using rentACar.Application.Features.Models.Queries.GetListModelByDynamic;
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

    [HttpPost("GetList/ByDynamic")]
    public async Task<ActionResult> GetListByDynamic([FromQuery] PageRequest pageRequest, [FromBody] Dynamic dynamic)
    {
        GetListModelByDynamicQuery getListModelByDynamicQuery = new GetListModelByDynamicQuery { PageRequest = pageRequest, Dynamic = dynamic };
        CustomResponseDto<ModelListModel> result = await Mediator.Send(getListModelByDynamicQuery);
        return Ok(result);
    }
}
