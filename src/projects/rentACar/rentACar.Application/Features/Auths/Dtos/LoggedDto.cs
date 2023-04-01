using Core.Application.ResponseTypes.Concrete;
using Core.Domain.Entities;
using Core.Domain.Enums;
using Core.Security.JWT;
using System.Net;

namespace rentACar.Application.Features.Auths.Dtos
{
    public class LoggedDto
    {
        public AccessToken? AccessToken { get; set; }
        public RefreshToken? RefreshToken { get; set; }
        public AuthenticatorType? RequiredAuthenticatorType { get; set; }

        public CustomResponseDto<LoggedResponseDto> CreateResponseDto()
        {
            return new CustomResponseDto<LoggedResponseDto>()
            {
                Data = new()
                {
                    AccessToken = AccessToken,
                    RefreshToken = RefreshToken is not null ? new LoggedRefreshTokenDto
                    {
                        Token = RefreshToken.Token,
                        Expires = RefreshToken.Expires
                    } : null,
                    RequiredAuthenticatorType = RequiredAuthenticatorType
                },
                IsSuccess = true,
                StatusCode = (int)HttpStatusCode.OK
            };
        }

        public class LoggedResponseDto
        {
            public AccessToken? AccessToken { get; set; }
            public LoggedRefreshTokenDto? RefreshToken { get; set; }
            public AuthenticatorType? RequiredAuthenticatorType { get; set; }
        }
    }
}