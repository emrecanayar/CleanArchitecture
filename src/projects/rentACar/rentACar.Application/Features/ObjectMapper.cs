using AutoMapper;
using rentACar.Application.Features.Brands.Profiles;
using rentACar.Application.Features.Models.Profiles;

namespace rentACar.Application.Features
{
    public static class ObjectMapper
    {
        private static readonly Lazy<IMapper> lazy = new Lazy<IMapper>(() =>
        {
            var config = new MapperConfiguration(configuration =>
            {
                configuration.AddProfile<BrandMappingProfiles>();
                configuration.AddProfile<ModelMappingProfiles>();
            });

            return config.CreateMapper();
        });

        public static IMapper Mapper => lazy.Value;
    }
}
