using Core.CrossCuttingConcerns.Exceptions;
using Core.Security.Entities;
using Core.Security.Enums;
using Core.Security.Hashing;
using rentACar.Application.Features.Auths.Constants;
using rentACar.Application.Services.Repositories;

namespace rentACar.Application.Features.Auths.Rules
{
    public class AuthBusinessRules
    {
        private readonly IUserRepository _userRepository;

        public AuthBusinessRules(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task EmailCantNotBeDuplicatedWhenRegistered(string email)
        {
            User? user = await _userRepository.GetAsync(x => x.Email == email);
            if (user is not null) throw new BusinessException("Mail already exists");
        }

        public Task UserShouldBeExists(User? user)
        {
            if (user == null) throw new BusinessException(AuthMessages.UserDontExists);
            return Task.CompletedTask;
        }

        public Task UserShouldNotBeHaveAuthenticator(User user)
        {
            if (user.AuthenticatorType != AuthenticatorType.None)
                throw new BusinessException(AuthMessages.UserHaveAlreadyAAuthenticator);
            return Task.CompletedTask;
        }

        public async Task UserPasswordShouldBeMatch(int id, string password)
        {
            User? user = await _userRepository.GetAsync(u => u.Id == id);
            if (!HashingHelper.VerifyPasswordHash(password, user.PasswordHash, user.PasswordSalt))
                throw new BusinessException(AuthMessages.PasswordDontMatch);
        }

        public Task RefreshTokenShouldBeExists(RefreshToken? refreshToken)
        {
            if (refreshToken == null) throw new BusinessException(AuthMessages.RefreshDontExists);
            return Task.CompletedTask;
        }

        public Task RefreshTokenShouldBeActive(RefreshToken refreshToken)
        {
            if (refreshToken.Revoked != null && DateTime.UtcNow >= refreshToken.Expires)
                throw new BusinessException(AuthMessages.InvalidRefreshToken);
            return Task.CompletedTask;
        }

        public Task OtpAuthenticatorThatVerifiedShouldNotBeExists(OtpAuthenticator? otpAuthenticator)
        {
            if (otpAuthenticator is not null && otpAuthenticator.IsVerified)
                throw new BusinessException(AuthMessages.AlreadyVerifiedOtpAuthenticatorIsExists);
            return Task.CompletedTask;
        }

        public Task EmailAuthenticatorShouldBeExists(EmailAuthenticator? emailAuthenticator)
        {
            if (emailAuthenticator is null) throw new BusinessException(AuthMessages.EmailAuthenticatorDontExists);
            return Task.CompletedTask;
        }

        public Task EmailAuthenticatorActivationKeyShouldBeExists(EmailAuthenticator emailAuthenticator)
        {
            if (emailAuthenticator.ActivationKey is null) throw new BusinessException(AuthMessages.EmailActivationKeyDontExists);
            return Task.CompletedTask;
        }
    }
}