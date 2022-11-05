using Core.Security.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using rentACar.Persistence.Configurations.Base;
using rentACar.Persistence.Constants;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
