using Swapy.Common.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Swapy.DAL.Configurations
{
    public class ClothesSizeConfiguration : IEntityTypeConfiguration<ClothesSize>
    {
        public void Configure(EntityTypeBuilder<ClothesSize> builder)
        {
            builder.ToTable("ClothesSizes");

            builder.HasKey(c => c.Id);

            builder.Property(c => c.Id).IsRequired();

            builder.Property(c => c.IsShoe)
                   .IsRequired()
                   .HasColumnType("BIT");

            builder.Property(c => c.IsChild)
                   .IsRequired()
                   .HasColumnType("BIT");
 
            builder.Property(c => c.Size)
                   .HasColumnType("NVARCHAR(32)")
                   .HasMaxLength(32)
                   .IsRequired();

            builder.HasMany(c => c.ClothesAttributes)  
                   .WithOne(c => c.ClothesSize)
                   .HasForeignKey(c => c.ClothesSizeId)
                   .IsRequired(false);
        }  
    }
} 
