using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using rentACar.Domain.Entities;
using rentACar.Persistence.Configurations.Base;
using rentACar.Persistence.Constants;

namespace rentACar.Persistence.Configurations
{
    public class BrandDocumentConfiguration : BaseConfiguration<BrandDocument>
    {
        public override void Configure(EntityTypeBuilder<BrandDocument> builder)
        {
            base.Configure(builder);
            builder.Property(x => x.BrandId).HasColumnName("BrandId");
            builder.HasOne(x => x.Brand).WithMany(x => x.BrandDocuments).HasForeignKey(x => x.BrandId);
            builder.ToTable(TableNameConstants.BRAND_DOCUMENT);
        }
    }
}
