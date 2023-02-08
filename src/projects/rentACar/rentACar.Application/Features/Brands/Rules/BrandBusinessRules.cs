using Core.Application.Rules;
using Core.CrossCuttingConcerns.Exceptions;
using Core.Persistence.Paging;
using rentACar.Application.Services.Repositories;
using rentACar.Domain.Entities;

namespace rentACar.Application.Features.Brands.Rules
{
    public class BrandBusinessRules : BaseBusinessRules
    {
        private readonly IBrandRepository _brandRepository;

        public BrandBusinessRules(IBrandRepository brandRepository)
        {
            _brandRepository = brandRepository;
        }

        public async Task BrandNameCanNotBeDuplicatedWhenInserted(string name)
        {
            IPaginate<Brand> result = await _brandRepository.GetListAsync(b => b.Name == name);
            if (result.Items.Any()) throw new BusinessException("Brand name exists");
        }

        public void BrandShouldExistWhenRequested(Brand brand)
        {
            if (brand == null) throw new BusinessException("Requested brand does not exist");
        }

    }
}
