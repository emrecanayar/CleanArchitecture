using AutoMapper;
using rentACar.Application.Features.Documents.Dtos;
using rentACar.Domain.Entities;

namespace rentACar.Application.Features.Documents.Profiles
{
    public class DocumentMappingProfiles : Profile
    {
        public DocumentMappingProfiles()
        {
            CreateMap<Document, DocumentDto>().ReverseMap();
        }
    }
}
