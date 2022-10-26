using Core.Application.ResponseTypes.Concrete;
using Core.Security.Entities;
using Core.Security.Enums;
using Core.Security.JWT;


namespace rentACar.Application.Features.Auths.Dtos
{
    public class LoggedDto
    {
        public AccessToken? AccessToken { get; set; }
        public RefreshToken? RefreshToken { get; set; }
        public AuthenticatorType? RequiredAuthenticatorType { get; set; }

        public CustomResponseDto<LoggedResponseDto> CreateResponseDto()
        {
            return new CustomResponseDto<LoggedResponseDto>() { Data = new() { AccessToken = AccessToken, RefreshToken = RefreshToken, RequiredAuthenticatorType = RequiredAuthenticatorType } };
        }

        public class LoggedResponseDto
        {
            public AccessToken? AccessToken { get; set; }
            public RefreshToken? RefreshToken { get; set; }
            public AuthenticatorType? RequiredAuthenticatorType { get; set; }
        }
    }
}
