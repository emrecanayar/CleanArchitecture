using Core.Persistence.Paging;
using Core.Security.Entities;
using Core.Security.JWT;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using rentACar.Application.Services.Repositories;

namespace rentACar.Application.Services.AuthService
{
    public class AuthManager : IAuthService
    {
        private readonly IUserOperationClaimRepository _userOperationClaimRepository;
        private readonly ITokenHelper _tokenHelper;
        private readonly IRefreshTokenRepository _refreshTokenRepository;
        private readonly TokenOptions _tokenOptions;

        public AuthManager(IUserOperationClaimRepository userOperationClaimRepository, ITokenHelper tokenHelper, IRefreshTokenRepository refreshTokenRepository, IConfiguration configuration)
        {
            _userOperationClaimRepository = userOperationClaimRepository;
            _tokenHelper = tokenHelper;
            _refreshTokenRepository = refreshTokenRepository;
            _tokenOptions = configuration.GetSection("TokenOptions").Get<TokenOptions>();
        }

        public async Task<RefreshToken> AddRefreshToken(RefreshToken refreshToken)
        {
            RefreshToken addedRefreshToken = await _refreshTokenRepository.AddAsync(refreshToken);
            return addedRefreshToken;
        }

        public async Task<AccessToken> CreateAccessToken(User user)
        {
            IPaginate<UserOperationClaim> userOperationClaims = await _userOperationClaimRepository.GetListAsync(x => x.UserId == user.Id, include: x => x.Include(x => x.OperationClaim));

            IList<OperationClaim> operationClaims = userOperationClaims.Items.Select(x => new OperationClaim
            {
                Id = x.OperationClaim.Id,
                Name = x.OperationClaim.Name
            }).ToList();

            AccessToken accessToken = await _tokenHelper.CreateToken(user, operationClaims);
            return accessToken;
        }

        public async Task<RefreshToken> CreateRefreshToken(User user, string ipAddress)
        {
            RefreshToken refreshToken = await _tokenHelper.CreateRefreshToken(user, ipAddress);
            return refreshToken;
        }

        public async Task DeleteOldRefreshTokens(int userId)
        {
            IList<RefreshToken> refreshTokens = (await _refreshTokenRepository.GetListAsync(r =>
                                               r.UserId == userId &&
                                               r.Revoked == null && r.Expires >= DateTime.UtcNow &&
                                               r.Created.AddDays(_tokenOptions.RefreshTokenTTL) <=
                                               DateTime.UtcNow)
                                          ).Items;
            foreach (RefreshToken refreshToken in refreshTokens) await _refreshTokenRepository.DeleteAsync(refreshToken);
        }

        public async Task<RefreshToken?> GetRefreshTokenByToken(string token)
        {
            RefreshToken? refreshToken = await _refreshTokenRepository.GetAsync(r => r.Token == token);
            return refreshToken;
        }
    }
}
