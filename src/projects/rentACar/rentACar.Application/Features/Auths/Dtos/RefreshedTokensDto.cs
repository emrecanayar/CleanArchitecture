using Core.Domain.Entities;
using Core.Security.JWT;

namespace rentACar.Application.Features.Auths.Dtos
{
    public class RefreshedTokensDto
    {
        public AccessToken AccessToken { get; set; }
        public RefreshToken RefreshToken { get; set; }
    }
}
