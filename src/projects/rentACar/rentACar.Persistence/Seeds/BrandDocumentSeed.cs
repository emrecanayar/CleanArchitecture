using Core.Persistence.ComplexTypes;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using rentACar.Domain.Entities;

namespace rentACar.Persistence.Seeds
{
    public class BrandDocumentSeed : IEntityTypeConfiguration<BrandDocument>
    {
        public void Configure(EntityTypeBuilder<BrandDocument> builder)
        {
            builder.HasData(
                new BrandDocument
                {
                    Id = 1,
                    BrandId = 1,
                    DocumentId = 1,
                    Status = RecordStatu.Active,
                    CreatedBy = "System",
                    CreatedDate = DateTime.Now,
                    ModifiedBy = "",
                    ModifiedDate = null,
                    IsDeleted = false
                });
        }
    }
}
