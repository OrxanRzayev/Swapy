using Swapy.Common.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Swapy.DAL.Configurations
{
    public class ClothesBrandConfiguration : IEntityTypeConfiguration<ClothesBrand>
    { 
        public void Configure(EntityTypeBuilder<ClothesBrand> builder)
        {
            builder.ToTable("ClothesBrands"); 
            builder.HasKey(c => c.Id);

            builder.Property(c => c.Id).IsRequired();

            builder.Property(s => s.Name)
                   .HasColumnType("NVARCHAR(32)")
                   .HasMaxLength(32)
                   .IsRequired(); 

            builder.HasMany(c => c.ClothesBrandsViews)
                   .WithOne(p => p.ClothesBrand)
                   .HasForeignKey(p => p.ClothesBrandId)
                   .IsRequired(false); 
        }
    } 
} 
 