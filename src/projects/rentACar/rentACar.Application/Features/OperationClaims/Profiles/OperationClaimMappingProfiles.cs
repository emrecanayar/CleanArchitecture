using AutoMapper;
using Core.Persistence.Paging;
using Core.Security.Entities;
using rentACar.Application.Features.OperationClaims.Commands.CreateOperationClaim;
using rentACar.Application.Features.OperationClaims.Commands.DeleteOperationClaim;
using rentACar.Application.Features.OperationClaims.Commands.UpdateOperationClaim;
using rentACar.Application.Features.OperationClaims.Dtos;
using rentACar.Application.Features.OperationClaims.Models;

namespace rentACar.Application.Features.OperationClaims.Profiles
{
    public class OperationClaimMappingProfiles : Profile
    {
        public OperationClaimMappingProfiles()
        {
            CreateMap<OperationClaim, CreateOperationClaimCommand>().ReverseMap();
            CreateMap<OperationClaim, CreatedOperationClaimDto>().ReverseMap();
            CreateMap<OperationClaim, UpdateOperationClaimCommand>().ReverseMap();
            CreateMap<OperationClaim, UpdatedOperationClaimDto>().ReverseMap();
            CreateMap<OperationClaim, DeleteOperationClaimCommand>().ReverseMap();
            CreateMap<OperationClaim, DeletedOperationClaimDto>().ReverseMap();
            CreateMap<OperationClaim, OperationClaimDto>().ReverseMap();
            CreateMap<OperationClaim, OperationClaimListDto>().ReverseMap();
            CreateMap<IPaginate<OperationClaim>, OperationClaimListModel>().ReverseMap();
        }
    }
}
