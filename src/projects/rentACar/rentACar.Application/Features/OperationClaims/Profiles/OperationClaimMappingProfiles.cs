using AutoMapper;
using Core.Security.Entities;
using rentACar.Application.Features.OperationClaims.Commands.CreateOperationClaim;
using rentACar.Application.Features.OperationClaims.Dtos;

namespace rentACar.Application.Features.OperationClaims.Profiles
{
    public class OperationClaimMappingProfiles : Profile
    {
        public OperationClaimMappingProfiles()
        {
            CreateMap<OperationClaim, CreatedOperationClaimDto>().ReverseMap();
            CreateMap<OperationClaim, CreateOperationClaimCommand>().ReverseMap();
        }
    }
}
