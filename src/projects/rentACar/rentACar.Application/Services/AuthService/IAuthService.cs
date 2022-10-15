using Core.Security.Entities;
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
    }
}
