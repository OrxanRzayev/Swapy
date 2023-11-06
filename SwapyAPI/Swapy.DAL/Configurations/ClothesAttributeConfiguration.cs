using Swapy.Common.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Swapy.DAL.Configurations
{
    public class ClothesAttributeConfiguration : IEntityTypeConfiguration<ClothesAttribute>
    {
        public void Configure(EntityTypeBuilder<ClothesAttribute> builder)
        {
            builder.ToTable("ClothesAttributes");
            builder.HasKey(p => p.Id);

            builder.Property(p => p.Id).IsRequired();

            builder.Property(x => x.IsNew)
                   .IsRequired()
                   .HasColumnType("BIT");
             
            builder.HasOne(p => p.ClothesSeason)
                   .WithMany(u => u.ClothesAttributes)
                   .HasForeignKey(p => p.ClothesSeasonId) 
                   .IsRequired();
             
            builder.HasOne(p => p.ClothesSize)
                   .WithMany(u => u.ClothesAttributes)
                   .HasForeignKey(p => p.ClothesSizeId)
                   .IsRequired();
            
            builder.HasOne(p => p.ClothesBrandView)
                   .WithMany(u => u.ClothesAttributes)
                   .HasForeignKey(p => p.ClothesBrandViewId)
                   .IsRequired();

            builder.HasOne(x => x.Product)
                   .WithOne(x => x.ClothesAttribute)
                   .HasForeignKey<Product>(x => x.ClothesAttributeId)
                   .OnDelete(DeleteBehavior.SetNull)
                   .IsRequired(false);
        }
    }
}
