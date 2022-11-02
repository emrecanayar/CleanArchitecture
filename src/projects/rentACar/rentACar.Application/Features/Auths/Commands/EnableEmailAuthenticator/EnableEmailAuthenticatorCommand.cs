﻿using Core.Mailing;
using Core.Security.Entities;
using Core.Security.Enums;
using MediatR;
using rentACar.Application.Features.Auths.Rules;
using rentACar.Application.Services.AuthService;
using rentACar.Application.Services.Repositories;
using rentACar.Application.Services.UserService;
using System.Web;

namespace rentACar.Application.Features.Auths.Commands.EnableEmailAuthenticator
{
    public class EnableEmailAuthenticatorCommand : IRequest
    {
        public int UserId { get; set; }
        public string VerifyEmailUrlPrefix { get; set; }

        public class EnableEmailAuthenticatorCommandHandler : IRequestHandler<EnableEmailAuthenticatorCommand>
        {
            private readonly IUserService _userService;
            private readonly IAuthService _authService;
            private readonly IEmailAuthenticatorRepository _emailAuthenticatorRepository;
            private readonly IMailService _mailService;
            private readonly AuthBusinessRules _authBusinessRules;

            public EnableEmailAuthenticatorCommandHandler(IUserService userService, AuthBusinessRules authBusinessRules, IAuthService authService, IEmailAuthenticatorRepository emailAuthenticatorRepository, IMailService mailService)
            {
                _userService = userService;
                _authBusinessRules = authBusinessRules;
                _authService = authService;
                _emailAuthenticatorRepository = emailAuthenticatorRepository;
                _mailService = mailService;
            }

            public async Task<Unit> Handle(EnableEmailAuthenticatorCommand request, CancellationToken cancellationToken)
            {
                User user = await _userService.GetById(request.UserId);
                await _authBusinessRules.UserShouldBeExists(user);
                await _authBusinessRules.UserShouldNotBeHaveAuthenticator(user);

                user.AuthenticatorType = AuthenticatorType.Email;
                await _userService.Update(user);

                EmailAuthenticator emailAuthenticator = await _authService.CreateEmailAuthenticator(user);
                EmailAuthenticator addedEmailAuthenticator = await _emailAuthenticatorRepository.AddAsync(emailAuthenticator);

                _mailService.SendMail(new Mail
                {
                    ToEmail = user.Email,
                    ToFullName = $"{user.FirstName} ${user.LastName}",
                    Subject = "Verify Your Email - RentACar",
                    TextBody =
                   $"Click on the link to verify your email: {request.VerifyEmailUrlPrefix}?ActivationKey={HttpUtility.UrlEncode(addedEmailAuthenticator.ActivationKey)}"
                });

                return Unit.Value;
            }
        }
    }
}