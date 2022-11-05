using Application.Features.UserOperationClaims.Profiles;
using Application.Features.Users.Profiles;
using AutoMapper;
using rentACar.Application.Features.Auths.Profiles;
using rentACar.Application.Features.Brands.Profiles;
using rentACar.Application.Features.Documents.Profiles;
using rentACar.Application.Features.Models.Profiles;
using rentACar.Application.Features.OperationClaims.Profiles;

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
                configuration.AddProfile<DocumentMappingProfiles>();
                configuration.AddProfile<AuthMappingProfiles>();
                configuration.AddProfile<UserMappingProfiles>();
                configuration.AddProfile<OperationClaimMappingProfiles>();
                configuration.AddProfile<UserOperationClaimMappingProfiles>();
            });

            return config.CreateMapper();
        });

        public static IMapper Mapper => lazy.Value;
    }
}
