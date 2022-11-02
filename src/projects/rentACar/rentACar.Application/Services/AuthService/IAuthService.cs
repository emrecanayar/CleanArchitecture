﻿using Core.Security.Entities;
using Core.Security.JWT;

namespace rentACar.Application.Services.AuthService
{
    public interface IAuthService
    {
        public Task<AccessToken> CreateAccessToken(User user);
        public Task<RefreshToken> CreateRefreshToken(User user, string ipAddress);
        public Task<RefreshToken> AddRefreshToken(RefreshToken refreshToken);
        public Task<RefreshToken?> GetRefreshTokenByToken(string token);
        public Task DeleteOldRefreshTokens(int userId);
        public Task RevokeDescendantRefreshTokens(RefreshToken refreshToken, string ipAddress, string reason);
        public Task RevokeRefreshToken(RefreshToken token, string ipAddress, string? reason = null,
                               string? replacedByToken = null);
        public Task<RefreshToken> RotateRefreshToken(User user, RefreshToken refreshToken, string ipAddress);
        public Task<EmailAuthenticator> CreateEmailAuthenticator(User user);
        public Task<OtpAuthenticator> CreateOtpAuthenticator(User user);
        public Task<string> ConvertSecretKeyToString(byte[] secretKey);
    }
}
