using Core.Security.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using rentACar.Persistence.Configurations.Base;
using rentACar.Persistence.Constants;

namespace rentACar.Persistence.Configurations
{
    public class EmailAuthenticatorConfiguration : BaseConfiguration<EmailAuthenticator>
    {
        public override void Configure(EntityTypeBuilder<EmailAuthenticator> builder)
        {
            base.Configure(builder);
            builder.Property(x => x.UserId).HasColumnName("UserId");
            builder.Property(x => x.ActivationKey).HasColumnName("ActivationKey").IsRequired(false).HasMaxLength(LengthContraints.ActivationKey);
            builder.Property(x => x.IsVerified).HasColumnName("IsVerified").IsRequired(true);
            builder.ToTable(TableNameConstants.EMAIL_AUTHENTICATOR);

        }
    }
}
