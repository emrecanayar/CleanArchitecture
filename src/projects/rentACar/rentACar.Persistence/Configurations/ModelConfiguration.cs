﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using rentACar.Domain.Entities;
using rentACar.Persistence.Configurations.Base;
using rentACar.Persistence.Constants;

namespace rentACar.Persistence.Configurations
{
    public class ModelConfiguration : BaseConfiguration<Model>
    {
        public override void Configure(EntityTypeBuilder<Model> builder)
        {
            base.Configure(builder);
            builder.Property(p => p.BrandId).HasColumnName("BrandId");
            builder.Property(p => p.Name).HasColumnName("Name").HasMaxLength(LengthContraints.NameMaxLength).IsRequired(true);
            builder.Property(p => p.DailyPrice).HasColumnName("DailyPrice").IsRequired(true);
            builder.Property(p => p.ImageUrl).HasColumnName("ImageUrl");
            builder.HasOne(p => p.Brand);
            builder.ToTable(TableNameConstants.MODEL);
        }
    }
}
