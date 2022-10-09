using Core.Security.Dtos;
using Core.Security.Entities;
using Microsoft.AspNetCore.Mvc;
using rentACar.Application.Features.Auths.Commands.Register;
using rentACar.Application.Features.Auths.Dtos;
using rentACar.WebAPI.Controllers.Base;

namespace rentACar.WebAPI.Controllers
{
    public class AuthController : BaseController
    {
        [HttpPost("Register")]
        public async Task<IActionResult> Register(UserForRegisterDto userForRegisterDto)
        {
            RegisterCommand registerCommand = new()
            {
                UserForRegister = userForRegisterDto,
                IpAddress = GetIpAddress()
            };

            RegisteredDto result = await Mediator.Send(registerCommand);
            SetRefreshTokenToCookie(result.RefreshToken);
            return Created("", result);
        }


        private void SetRefreshTokenToCookie(RefreshToken refreshToken)
        {
            CookieOptions cookieOptions = new()
            {
                HttpOnly = true,
                Expires = DateTime.Now.AddDays(7)
            };

            Response.Cookies.Append("refreshToken", refreshToken.Token, cookieOptions);
        }
    }
}

