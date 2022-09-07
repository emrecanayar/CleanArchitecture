using AutoMapper;
using Core.Persistence.Paging;
using rentACar.Application.Features.Models.Dtos;
using rentACar.Application.Features.Models.Models;
using rentACar.Domain.Entities;

namespace rentACar.Application.Features.Models.Profiles
{
    public class ModelMappingProfiles : Profile
    {
        public ModelMappingProfiles()
        {
            CreateMap<IPaginate<Model>, ModelListModel>().ReverseMap();
            CreateMap<Model, ModelListDto>().ForMember(c => c.BrandName, opt => opt.MapFrom(c => c.Brand.Name)).ReverseMap();

        }
    }
}
