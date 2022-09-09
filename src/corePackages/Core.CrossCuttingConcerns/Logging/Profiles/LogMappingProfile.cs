using AutoMapper;
using Core.CrossCuttingConcerns.Logging.DbLog.Dto;
using Core.CrossCuttingConcerns.Logging.DbLog.Models;

namespace Core.CrossCuttingConcerns.Logging.Profiles
{
    public class LogMappingProfile : Profile
    {
        public LogMappingProfile()
        {
            CreateMap<Log, LogDto>().ReverseMap();
        }
    }
}
