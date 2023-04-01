using Core.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using rentACar.Persistence.Configurations.Base;
using rentACar.Persistence.Constants;

namespace rentACar.Persistence.Configurations
{
    public class UserOperationClaimConfiguration : BaseConfiguration<UserOperationClaim>
    {
        public override void Configure(EntityTypeBuilder<UserOperationClaim> builder)
        {
            base.Configure(builder);
            builder.Property(x => x.UserId).HasColumnName("UserId");
            builder.Property(x => x.OperationClaimId).HasColumnName("OperationClaimId");
            builder.HasIndex(x => new { x.UserId, x.OperationClaimId }, "UK_UserOperationClaims_UserId_OperationClaimId").IsUnique();
            builder.HasOne(x => x.User).WithMany(x => x.UserOperationClaims).HasForeignKey(x => x.UserId);
            builder.ToTable(TableNameConstants.USER_OPERATION_CLAIM);
        }
    }
}
