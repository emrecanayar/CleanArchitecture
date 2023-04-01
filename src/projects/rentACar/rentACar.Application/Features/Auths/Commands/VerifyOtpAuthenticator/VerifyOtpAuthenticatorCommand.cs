using Core.Domain.Entities;
using Core.Domain.Enums;
using MediatR;
using rentACar.Application.Features.Auths.Rules;
using rentACar.Application.Services.AuthService;
using rentACar.Application.Services.Repositories;
using rentACar.Application.Services.UserService;

namespace rentACar.Application.Features.Auths.Commands.VerifyOtpAuthenticator
{
    public class VerifyOtpAuthenticatorCommand : IRequest
    {
        public int UserId { get; set; }
        public string ActivationCode { get; set; }

        public class VerifyOtpAuthenticatorCommandHandler : IRequestHandler<VerifyOtpAuthenticatorCommand>
        {
            private readonly IOtpAuthenticatorRepository _otpAuthenticatorRepository;
            private readonly AuthBusinessRules _authBusinessRules;
            private readonly IUserService _userService;
            private readonly IAuthService _authService;

            public VerifyOtpAuthenticatorCommandHandler(IOtpAuthenticatorRepository otpAuthenticatorRepository, AuthBusinessRules authBusinessRules, IUserService userService, IAuthService authService)
            {
                _otpAuthenticatorRepository = otpAuthenticatorRepository;
                _authBusinessRules = authBusinessRules;
                _userService = userService;
                _authService = authService;
            }

            public async Task<Unit> Handle(VerifyOtpAuthenticatorCommand request, CancellationToken cancellationToken)
            {
                OtpAuthenticator? otpAuthenticator = await _otpAuthenticatorRepository.GetAsync(x => x.UserId == request.UserId);
                await _authBusinessRules.OtpAuthenticatorShouldBeExists(otpAuthenticator);

                User user = await _userService.GetById(request.UserId);
                user.AuthenticatorType = AuthenticatorType.Otp;

                await _authService.VerifyAuthenticatorCode(user, request.ActivationCode);
                await _otpAuthenticatorRepository.UpdateAsync(otpAuthenticator);
                await _userService.Update(user);

                return Unit.Value;
            }
        }
    }
}
