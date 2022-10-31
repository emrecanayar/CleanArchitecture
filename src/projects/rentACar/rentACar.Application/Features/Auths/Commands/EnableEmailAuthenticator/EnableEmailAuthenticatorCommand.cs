using Core.Security.Entities;
using Core.Security.Enums;
using MediatR;
using rentACar.Application.Features.Auths.Rules;
using rentACar.Application.Services.AuthService;
using rentACar.Application.Services.Repositories;
using rentACar.Application.Services.UserService;

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
            private readonly AuthBusinessRules _authBusinessRules;

            public EnableEmailAuthenticatorCommandHandler(IUserService userService, AuthBusinessRules authBusinessRules, IAuthService authService, IEmailAuthenticatorRepository emailAuthenticatorRepository)
            {
                _userService = userService;
                _authBusinessRules = authBusinessRules;
                _authService = authService;
                _emailAuthenticatorRepository = emailAuthenticatorRepository;
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

                //E-mail Service Added.

                return Unit.Value;
            }
        }
    }
}
