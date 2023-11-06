using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Swapy.Common.Entities;

namespace Swapy.DAL.Configurations
{
    public class UserTokenConfiguration : IEntityTypeConfiguration<UserToken>
    {
        public void Configure(EntityTypeBuilder<UserToken> builder)
        {
            builder.ToTable("UserTokens");
            builder.HasKey(r => r.RefreshToken);

            builder.Property(ur => ur.RefreshToken).IsRequired();

            builder.Property(ur => ur.AccessToken).IsRequired();

            builder.HasOne(ur => ur.User)
                   .WithOne(u => u.UserToken)
                   .HasForeignKey<User>(u => u.UserTokenId)
                   .OnDelete(DeleteBehavior.SetNull)
                   .IsRequired(false);
        }
    }
}
