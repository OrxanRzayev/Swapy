using Swapy.Common.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Swapy.DAL.Configurations
{
    public class LikeConfiguration : IEntityTypeConfiguration<Like>
    {
        public void Configure(EntityTypeBuilder<Like> builder)
        {
            builder.ToTable("Likes");
            builder.HasKey(l => l.Id);

            builder.Property(l => l.Id).IsRequired();

            builder.HasOne(l => l.Liker)
                   .WithMany(u => u.Likes)
                   .HasForeignKey(l => l.LikerId)
                   .OnDelete(DeleteBehavior.SetNull)
                   .IsRequired(false);

            builder.HasOne(l => l.UserLike)
                   .WithOne(ul => ul.Like)
                   .HasForeignKey<UserLike>(ul => ul.LikeId)
                   .OnDelete(DeleteBehavior.Cascade)
                   .IsRequired(false);
        }
    }
}
