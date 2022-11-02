using Core.Security.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using rentACar.Persistence.Configurations.Base;
using rentACar.Persistence.Constants;

namespace rentACar.Persistence.Configurations
{
    public class OtpAuthenticatorConfiguration : BaseConfiguration<OtpAuthenticator>
    {
        public override void Configure(EntityTypeBuilder<OtpAuthenticator> builder)
        {
            base.Configure(builder);
            base.Configure(builder);
            builder.Property(x => x.UserId).HasColumnName("UserId");
            builder.Property(x => x.SecretKey).HasColumnName("SecretKey").IsRequired(true).HasMaxLength(LengthContraints.ActivationKey);
            builder.Property(x => x.IsVerified).HasColumnName("IsVerified").IsRequired(true);
            builder.ToTable(TableNameConstants.OTP_AUTHENTICATOR);
        }
    }
}
