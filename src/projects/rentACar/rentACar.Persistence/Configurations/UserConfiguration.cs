using Core.Security.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using rentACar.Persistence.Configurations.Base;
using rentACar.Persistence.Constants;

namespace rentACar.Persistence.Configurations
{
    public class UserConfiguration : BaseConfiguration<User>
    {
        public override void Configure(EntityTypeBuilder<User> builder)
        {
            base.Configure(builder);
            builder.Property(x => x.FirstName).HasColumnName("FirstName").IsRequired(true).HasMaxLength(LengthContraints.NameMaxLength);
            builder.Property(x => x.LastName).HasColumnName("LastName").IsRequired(true).HasMaxLength(LengthContraints.NameMaxLength);
            builder.Property(x => x.Email).HasColumnName("Email").IsRequired(true).HasMaxLength(LengthContraints.EmailMaxLength);
            builder.Property(x => x.PasswordHash).HasColumnName("PasswordHash").IsRequired(true).HasMaxLength(LengthContraints.PasswordMaxLength);
            builder.Property(x => x.PasswordSalt).HasColumnName("PasswordSalt").HasMaxLength(LengthContraints.PasswordMaxLength);
            builder.Property(x => x.AuthenticatorType).HasColumnName("AuthenticatorType").HasConversion<int>();
            builder.Property(x => x.CultureType).HasColumnName("CultureType").HasConversion<int>();
            builder.HasMany(x => x.UserOperationClaims);
            builder.HasMany(x => x.RefreshTokens);
            builder.ToTable(TableNameConstants.USER);
        }
    }
}
