﻿using Core.Application.ResponseTypes.Concrete;
using Core.Domain.Entities;
using Core.Domain.Enums;
using Core.Security.Dtos;
using Core.Security.JWT;
using MediatR;
using rentACar.Application.Features.Auths.Dtos;
using rentACar.Application.Features.Auths.Rules;
using rentACar.Application.Services.AuthService;
using rentACar.Application.Services.UserService;
using System.Net;

namespace rentACar.Application.Features.Auths.Commands.Login
{
    public class LoginCommand : IRequest<CustomResponseDto<LoggedDto>>
    {
        public UserForLoginDto UserForLoginDto { get; set; }
        public string IPAddress { get; set; }

        public class LoginCommandHandler : IRequestHandler<LoginCommand, CustomResponseDto<LoggedDto>>
        {
            private readonly IUserService _userService;
            private readonly IAuthService _authService;
            private readonly AuthBusinessRules _authBusinessRules;

            public LoginCommandHandler(IUserService userService, IAuthService authService, AuthBusinessRules authBusinessRules)
            {
                _userService = userService;
                _authService = authService;
                _authBusinessRules = authBusinessRules;
            }

            public async Task<CustomResponseDto<LoggedDto>> Handle(LoginCommand request, CancellationToken cancellationToken)
            {
                User? user = await _userService.GetByEmail(request.UserForLoginDto.Email);
                await _authBusinessRules.UserShouldBeExists(user);
                await _authBusinessRules.UserPasswordShouldBeMatch(user.Id, request.UserForLoginDto.Password);

                LoggedDto loggedDto = new();

                if (user.AuthenticatorType is not AuthenticatorType.None)
                {
                    if (request.UserForLoginDto.AuthenticatorCode is null)
                    {
                        await _authService.SendAuthenticatorCode(user);
                        loggedDto.RequiredAuthenticatorType = user.AuthenticatorType;
                        return CustomResponseDto<LoggedDto>.Success((int)HttpStatusCode.OK, loggedDto, isSuccess: true);
                    }

                    await _authService.VerifyAuthenticatorCode(user, request.UserForLoginDto.AuthenticatorCode);
                }


                AccessToken createdAccessToken = await _authService.CreateAccessToken(user);
                RefreshToken createdRefreshToken = await _authService.CreateRefreshToken(user, request.IPAddress);
                RefreshToken addedRefreshToken = await _authService.AddRefreshToken(createdRefreshToken);

                await _authService.DeleteOldRefreshTokens(user.Id);

                loggedDto.AccessToken = createdAccessToken;
                loggedDto.RefreshToken = addedRefreshToken;
                return CustomResponseDto<LoggedDto>.Success((int)HttpStatusCode.OK, loggedDto, isSuccess: true);
            }
        }
    }
}
