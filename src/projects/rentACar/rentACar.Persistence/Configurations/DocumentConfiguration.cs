using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using rentACar.Domain.Entities;
using rentACar.Persistence.Configurations.Base;
using rentACar.Persistence.Constants;

namespace rentACar.Persistence.Configurations
{
    public class DocumentConfiguration : BaseConfiguration<Document>
    {
        public override void Configure(EntityTypeBuilder<Document> builder)
        {
            base.Configure(builder);
            builder.Property(x => x.Token).HasColumnName("Token").IsRequired(true).HasMaxLength(LengthContraints.TokenMaxLength);
            builder.ToTable(TableNameConstants.DOCUMENT);
        }
    }
}
