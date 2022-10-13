using Core.Application.Pipelines.Logging;
using MediatR;
using rentACar.Application.Features.Brands.Dtos;
using rentACar.Application.Features.Brands.Rules;
using rentACar.Application.Services.Repositories;
using rentACar.Domain.Entities;

namespace rentACar.Application.Features.Brands.Commands.CreateBrand
{
    public class CreateBrandCommand : IRequest<CreatedBrandDto>, ILoggableRequest
    {
        public string Name { get; set; }

        public class CreateBrandCommandHandler : IRequestHandler<CreateBrandCommand, CreatedBrandDto>
        {
            private readonly IBrandRepository _brandRepository;
            private readonly BrandBusinessRules _brandBusinessRules;

            public CreateBrandCommandHandler(IBrandRepository brandRepository, BrandBusinessRules brandBusinessRules)
            {
                _brandRepository = brandRepository;
                _brandBusinessRules = brandBusinessRules;
            }

            public async Task<CreatedBrandDto> Handle(CreateBrandCommand request, CancellationToken cancellationToken)
            {
                await _brandBusinessRules.BrandNameCanNotBeDuplicatedWhenInserted(request.Name);

                Brand mappedBrand = ObjectMapper.Mapper.Map<Brand>(request);
                Brand createdBrand = await _brandRepository.AddAsync(mappedBrand);
                var createdBrandDto = ObjectMapper.Mapper.Map<CreatedBrandDto>(createdBrand);
                return createdBrandDto;
            }
        }
    }
}
