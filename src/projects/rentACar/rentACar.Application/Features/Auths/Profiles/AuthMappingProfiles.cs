using AutoMapper;
using Core.Domain.Entities;
using rentACar.Application.Features.Auths.Dtos;

namespace rentACar.Application.Features.Auths.Profiles
{
    public class AuthMappingProfiles : Profile
    {
        public AuthMappingProfiles()
        {
            CreateMap<RefreshToken, RevokedTokenDto>().ReverseMap();
        }
    }
}
