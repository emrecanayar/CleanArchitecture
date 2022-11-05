using Core.Test.Helpers;
using Moq;
using rentACar.Application.Services.Repositories;
using rentACar.Domain.Entities;

namespace Application.Tests.Mocks.Repositories
{
    public class BrandMockRepository
    {
        public Mock<IBrandRepository> GetRepository()
        {
            var brands = new List<Brand>
            {
                new Brand
                {
                    Id = 1,
                    Name =  "Mercedes"
                },
                new Brand
                {
                    Id = 2,
                    Name =  "BMW"
                },
            };

            return MockRepositoryHelper.GetRepository<IBrandRepository, Brand>(brands);
        }
    }
}
