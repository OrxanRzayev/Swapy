using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Swapy.Common.Entities;

namespace Swapy.DAL.Configurations
{
    public class FavoriteProductConfiguration : IEntityTypeConfiguration<FavoriteProduct>
    {
        public void Configure(EntityTypeBuilder<FavoriteProduct> builder)
        {
            builder.ToTable("FavoriteProducts");
            builder.HasKey(f => f.Id);

            builder.Property(f => f.Id).IsRequired();

            builder.HasOne(f => f.Product)
                   .WithMany(p => p.FavoriteProducts)
                   .HasForeignKey(f => f.ProductId)
                   .IsRequired();

            builder.HasOne(f => f.User)
                   .WithMany(u => u.FavoriteProducts)
                   .HasForeignKey(f => f.UserId)
                   .IsRequired();
        }
    }
}
