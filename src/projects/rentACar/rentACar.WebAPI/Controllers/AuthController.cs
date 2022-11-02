using Core.Application.ResponseTypes.Concrete;
using Core.Security.Dtos;
using Core.Security.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using rentACar.Application.Features.Auths.Commands.EnableEmailAuthenticator;
using rentACar.Application.Features.Auths.Commands.EnableOtpAuthenticator;
using rentACar.Application.Features.Auths.Commands.Login;
using rentACar.Application.Features.Auths.Commands.RefreshTokens;
using rentACar.Application.Features.Auths.Commands.Register;
using rentACar.Application.Features.Auths.Commands.RevokeToken;
using rentACar.Application.Features.Auths.Commands.VerifyEmailAuthenticator;
using rentACar.Application.Features.Auths.Dtos;
using rentACar.WebAPI.Controllers.Base;

namespace rentACar.WebAPI.Controllers
{
    public class AuthController : BaseController
    {
        private readonly WebAPIConfiguration _configuration;

        public AuthController(IConfiguration configuration)
        {
            _configuration = configuration.GetSection("WebAPIConfiguration").Get<WebAPIConfiguration>();
        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromBody] UserForLoginDto userForLoginDto)
        {
            LoginCommand loginCommand = new() { UserForLoginDto = userForLoginDto, IPAddress = getIpAddress() };

            CustomResponseDto<LoggedDto> result = await Mediator.Send(loginCommand);

            if (result.Data.RefreshToken is not null) setRefreshTokenToCookie(result.Data.RefreshToken);

            return Ok(result.Data.CreateResponseDto());
        }

        [HttpPost("Register")]
        public async Task<IActionResult> Register(UserForRegisterDto userForRegisterDto)
        {
            RegisterCommand registerCommand = new()
            {
                UserForRegister = userForRegisterDto,
                IpAddress = getIpAddress()
            };

            RegisteredDto result = await Mediator.Send(registerCommand);
            setRefreshTokenToCookie(result.RefreshToken);
            return Created("", result.AccessToken);
        }

        [HttpGet("RefreshToken")]
        public async Task<IActionResult> RefreshToken()
        {
            RefreshTokenCommand refreshTokenCommand = new()
            { RefreshToken = getRefreshTokenFromCookies(), IPAddress = getIpAddress() };
            RefreshedTokensDto result = await Mediator.Send(refreshTokenCommand);
            setRefreshTokenToCookie(result.RefreshToken);
            return Created("", result.AccessToken);
        }

        [HttpPut("RevokeToken")]
        public async Task<IActionResult> RevokeToken(
       [FromBody(EmptyBodyBehavior = EmptyBodyBehavior.Allow)]
        string? refreshToken)
        {
            RevokeTokenCommand revokeTokenCommand = new()
            {
                Token = refreshToken ?? getRefreshTokenFromCookies(),
                IPAddress = getIpAddress()
            };
            RevokedTokenDto result = await Mediator.Send(revokeTokenCommand);
            return Ok(result);
        }

        [HttpGet("EnableEmailAuthenticator")]
        public async Task<IActionResult> EnableEmailAuthenticator()
        {
            EnableEmailAuthenticatorCommand enableEmailAuthenticatorCommand = new()
            {
                UserId = getUserIdFromRequest(),
                VerifyEmailUrlPrefix = $"{_configuration.APIDomain}/Auth/VerifyEmailAuthenticator"
            };
            await Mediator.Send(enableEmailAuthenticatorCommand);

            return Ok();
        }

        [HttpGet("EnableOtpAuthenticator")]
        public async Task<IActionResult> EnableOtpAuthenticator()
        {
            EnableOtpAuthenticatorCommand enableOtpAuthenticatorCommand = new()
            {
                UserId = getUserIdFromRequest()
            };
            EnabledOtpAuthenticatorDto result = await Mediator.Send(enableOtpAuthenticatorCommand);

            return Ok(result);
        }

        [HttpGet("VerifyEmailAuthenticator")]
        public async Task<IActionResult> VerifyEmailAuthenticator(
       [FromQuery] VerifyEmailAuthenticatorCommand verifyEmailAuthenticatorCommand)
        {
            await Mediator.Send(verifyEmailAuthenticatorCommand);
            return Ok();
        }

        private string? getRefreshTokenFromCookies()
        {
            return Request.Cookies["refreshToken"];
        }

        private void setRefreshTokenToCookie(RefreshToken refreshToken)
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