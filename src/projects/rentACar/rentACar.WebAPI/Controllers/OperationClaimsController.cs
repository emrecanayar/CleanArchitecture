using Microsoft.AspNetCore.Mvc;
using rentACar.Application.Features.OperationClaims.Commands.CreateOperationClaim;
using rentACar.Application.Features.OperationClaims.Dtos;
using rentACar.WebAPI.Controllers.Base;

namespace rentACar.WebAPI.Controllers
{
    public class OperationClaimsController : BaseController
    {
        [HttpPost]
        public async Task<IActionResult> Add([FromBody] CreateOperationClaimCommand createOperationClaimCommand)
        {
            CreatedOperationClaimDto result = await Mediator.Send(createOperationClaimCommand);
            return Created("", result);
        }
    }
}
