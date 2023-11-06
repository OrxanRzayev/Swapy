using Swapy.Common.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Swapy.DAL.Configurations
{
    public class ClothesBrandViewConfiguration : IEntityTypeConfiguration<ClothesBrandView>
    {
        public void Configure(EntityTypeBuilder<ClothesBrandView> builder)
        {
            builder.ToTable("ClothesBrandsViews");
            builder.HasKey(c => c.Id);

            builder.Property(c => c.Id).IsRequired();

            builder.HasOne(c => c.ClothesView)
                   .WithMany(c => c.ClothesBrandViews)
                   .HasForeignKey(c => c.ClothesViewId)
                   .IsRequired();

            builder.HasOne(c => c.ClothesBrand)
                   .WithMany(c => c.ClothesBrandsViews)
                   .HasForeignKey(c => c.ClothesBrandId)
                   .IsRequired();

            builder.HasMany(s => s.ClothesAttributes)
                   .WithOne(c => c.ClothesBrandView) 
                   .HasForeignKey(s => s.ClothesBrandViewId)
                   .IsRequired(false);  
        } 
    }
}

 