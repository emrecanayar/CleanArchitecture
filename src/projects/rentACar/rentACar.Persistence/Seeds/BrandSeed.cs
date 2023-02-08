using Core.Domain.ComplexTypes;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using rentACar.Domain.Entities;

namespace rentACar.Persistence.Seeds
{
    public class BrandSeed : IEntityTypeConfiguration<Brand>
    {
        public void Configure(EntityTypeBuilder<Brand> builder)
        {
            builder.HasData(
                  new Brand { Id = 1, Name = "BMW", Status = RecordStatu.Active, CreatedBy = "System", CreatedDate = DateTime.Now, ModifiedBy = "", ModifiedDate = null, IsDeleted = false },
                  new Brand { Id = 2, Name = "Mercedes", Status = RecordStatu.Active, CreatedBy = "System", CreatedDate = DateTime.Now, ModifiedBy = "", ModifiedDate = null, IsDeleted = false },
                  new Brand { Id = 3, Name = "Audi", Status = RecordStatu.Active, CreatedBy = "System", CreatedDate = DateTime.Now, ModifiedBy = "", ModifiedDate = null, IsDeleted = false });
        }
    }
}
