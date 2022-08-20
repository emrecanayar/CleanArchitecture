using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using rentACar.Domain.Entities;
using rentACar.Persistence.Configurations.Base;
using rentACar.Persistence.Constants;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace rentACar.Persistence.Configurations
{
    public class BrandConfiguration : BaseConfiguration<Brand>
    {
        public override void Configure(EntityTypeBuilder<Brand> builder)
        {
            base.Configure(builder);
            builder.Property(x => x.Name).HasColumnName("Name").IsRequired(true).HasMaxLength(LengthContraints.NameMaxLength);
            builder.ToTable(TableNameConstants.BRAND);
        }
    }
}
