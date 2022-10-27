using Core.Security.Entities;
using MediatR;
using rentACar.Application.Features.Auths.Dtos;
using rentACar.Application.Features.Auths.Rules;
using rentACar.Application.Services.AuthService;

namespace rentACar.Application.Features.Auths.Commands.RevokeToken
{
    public class RevokeTokenCommand : IRequest<RevokedTokenDto>
    {
        public string Token { get; set; }
        public string IPAddress { get; set; }

        public class RevokeTokenCommandHanlder : IRequestHandler<RevokeTokenCommand, RevokedTokenDto>
        {
            private readonly IAuthService _authService;
            private readonly AuthBusinessRules _authBusinessRules;

            public RevokeTokenCommandHanlder(IAuthService authService, AuthBusinessRules authBusinessRules)
            {
                _authService = authService;
                _authBusinessRules = authBusinessRules;
            }

            public async Task<RevokedTokenDto> Handle(RevokeTokenCommand request, CancellationToken cancellationToken)
            {
                RefreshToken? refreshToken = await _authService.GetRefreshTokenByToken(request.Token);
                await _authBusinessRules.RefreshTokenShouldBeExists(refreshToken);
                await _authBusinessRules.RefreshTokenShouldBeActive(refreshToken);

                await _authService.RevokeRefreshToken(refreshToken, request.IPAddress, "Revoked without replacement");
                RevokedTokenDto revokedTokenDto = ObjectMapper.Mapper.Map<RevokedTokenDto>(refreshToken);
                return revokedTokenDto;
            }
        }
    }
}
