using MediatR;
using rentACar.Application.Features.Brands.Dtos;
using rentACar.Application.Features.Brands.Rules;
using rentACar.Application.Services.Repositories;
using rentACar.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace rentACar.Application.Features.Brands.Queries.GetByIdBrand
{
    public class GetByIdBrandQuery : IRequest<BrandGetByIdDto>
    {
        public int Id { get; set; }

        public class GetByIdBrandQueryHandler : IRequestHandler<GetByIdBrandQuery, BrandGetByIdDto>
        {
            private readonly IBrandRepository _brandRepository;
            private readonly BrandBusinessRules _brandBusinessRules;

            public GetByIdBrandQueryHandler(IBrandRepository brandRepository, BrandBusinessRules brandBusinessRules)
            {
                _brandRepository = brandRepository;
                _brandBusinessRules = brandBusinessRules;
            }

            public async Task<BrandGetByIdDto> Handle(GetByIdBrandQuery request, CancellationToken cancellationToken)
            {
                Brand? brand = await _brandRepository.GetAsync(b => b.Id == request.Id);
                _brandBusinessRules.BrandShouldExistWhenRequested(brand);
                BrandGetByIdDto brandGetByIdDto = ObjectMapper.Mapper.Map<BrandGetByIdDto>(brand);
                return brandGetByIdDto;
            }
        }
    }
}
