using Application.Tests.Mocks.Repositories;
using AutoMapper;
using Core.Application.Requests;
using Core.CrossCuttingConcerns.Exceptions;
using Moq;
using rentACar.Application.Features.Brands.Commands.CreateBrand;
using rentACar.Application.Features.Brands.Profiles;
using rentACar.Application.Features.Brands.Queries.GetByIdBrand;
using rentACar.Application.Features.Brands.Queries.GetListBrand;
using rentACar.Application.Features.Brands.Queries.GetListBrandPaginate;
using rentACar.Application.Features.Brands.Rules;
using rentACar.Application.Services.Repositories;
using Xunit;
using static rentACar.Application.Features.Brands.Commands.CreateBrand.CreateBrandCommand;
using static rentACar.Application.Features.Brands.Queries.GetByIdBrand.GetByIdBrandQuery;
using static rentACar.Application.Features.Brands.Queries.GetListBrand.GetListBrandQuery;
using static rentACar.Application.Features.Brands.Queries.GetListBrandPaginate.GetListBrandPaginateQuery;

namespace Application.Tests.FeaturesTests.Brands
{
    public class BrandsTests
    {
        private readonly Mock<IBrandRepository> _mockBrandRepository;
        private readonly BrandBusinessRules _brandBusinessRules;

        public BrandsTests()
        {
            _mockBrandRepository = new BrandMockRepository().GetRepository();
            _brandBusinessRules = new BrandBusinessRules(_mockBrandRepository.Object);

            var mapperConfig = new MapperConfiguration(c =>
            {
                c.AddProfile<BrandMappingProfiles>();
            });
        }

        [Fact]
        public async Task AddBrandWhenNotDuplicated()
        {
            CreateBrandCommandHandler handler = new CreateBrandCommandHandler(_mockBrandRepository.Object, _brandBusinessRules);
            CreateBrandCommand command = new CreateBrandCommand();
            command.Name = "Audi";

            var result = await handler.Handle(command, CancellationToken.None);

            Assert.Equal("Audi", result.Name);
        }

        [Fact]
        public async Task AddBrandWhenDuplicated()
        {
            CreateBrandCommandHandler handler = new CreateBrandCommandHandler(_mockBrandRepository.Object, _brandBusinessRules);
            CreateBrandCommand command = new CreateBrandCommand();
            command.Name = "BMW";

            await Assert.ThrowsAsync<BusinessException>(async () => await handler.Handle(command, CancellationToken.None));
        }

        [Fact]
        public async Task GetAllBrandsForPaginate()
        {
            GetListBrandPaginateQueryHandler handler = new GetListBrandPaginateQueryHandler(_mockBrandRepository.Object);
            GetListBrandPaginateQuery query = new GetListBrandPaginateQuery();
            query.PageRequest = new PageRequest
            {
                Page = 0,
                PageSize = 3
            };

            var result = await handler.Handle(query, CancellationToken.None);

            Assert.Equal(2, result.Data.Items.Count);
        }

        [Fact]
        public async Task GetByIdBrandWhenExistsBrand()
        {
            GetByIdBrandQueryHandler handler = new GetByIdBrandQueryHandler(_mockBrandRepository.Object, _brandBusinessRules);
            GetByIdBrandQuery query = new GetByIdBrandQuery();
            query.Id = 1;

            var result = await handler.Handle(query, CancellationToken.None);

            Assert.Equal("Mercedes", result.Data.Name);
        }

        [Fact]
        public async Task GetByIdBrandWhenNotExistsBrand()
        {
            GetByIdBrandQueryHandler handler = new GetByIdBrandQueryHandler(_mockBrandRepository.Object, _brandBusinessRules);
            GetByIdBrandQuery query = new GetByIdBrandQuery();
            query.Id = 6;

            await Assert.ThrowsAsync<BusinessException>(async () => await handler.Handle(query, CancellationToken.None));
        }
    }
}