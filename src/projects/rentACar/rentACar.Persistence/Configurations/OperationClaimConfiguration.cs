﻿using Core.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using rentACar.Persistence.Configurations.Base;
using rentACar.Persistence.Constants;

namespace rentACar.Persistence.Configurations
{
    public class OperationClaimConfiguration : BaseConfiguration<OperationClaim>
    {
        public override void Configure(EntityTypeBuilder<OperationClaim> builder)
        {
            base.Configure(builder);
            builder.Property(x => x.Name).HasColumnName("Name").IsRequired(true).HasMaxLength(LengthContraints.NameMaxLength);
            builder.HasIndex(x => x.Name, "UK_OperationClaims_Name").IsUnique();
            builder.ToTable(TableNameConstants.OPERATION_CLAIM);
        }
    }
}
