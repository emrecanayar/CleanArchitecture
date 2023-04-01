using Core.Domain.Entities;

namespace Core.Security.JWT
{
    public interface ITokenHelper
    {
        Task<AccessToken> CreateToken(User user, IList<OperationClaim> operationClaims);
        Task<RefreshToken> CreateRefreshToken(User user, string ipAddress);
    }
}
