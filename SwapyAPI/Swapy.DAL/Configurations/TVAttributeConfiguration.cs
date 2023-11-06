using Swapy.Common.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Swapy.DAL.Configurations
{
    public class TVAttributeConfiguration : IEntityTypeConfiguration<TVAttribute>
    {
        public void Configure(EntityTypeBuilder<TVAttribute> builder)
        {
            builder.ToTable("TVAttributes");
            builder.HasKey(t => t.Id);

            builder.Property(t => t.Id).IsRequired();
             
            builder.Property(t => t.IsNew)
                   .IsRequired()
                   .HasColumnType("BIT");
            
            builder.Property(t => t.IsSmart)
                   .IsRequired()
                   .HasColumnType("BIT");

            builder.HasOne(t => t.TVBrand)
                   .WithMany(t => t.TVAttributes)
                   .HasForeignKey(t => t.TVBrandId)
                   .IsRequired();

            builder.HasOne(t => t.TVType)
                   .WithMany(t => t.TVAttributes)
                   .HasForeignKey(t => t.TVTypeId)
                   .IsRequired();

            builder.HasOne(t => t.ScreenResolution)
                   .WithMany(t => t.TVAttributes)
                   .HasForeignKey(t  => t.ScreenResolutionId)
                   .IsRequired();
             
            builder.HasOne(t => t.ScreenDiagonal)
                   .WithMany(t => t.TVAttributes)
                   .HasForeignKey(t => t.ScreenDiagonalId)
                   .IsRequired();
             
            builder.HasOne(t => t.Product)
                   .WithOne(t => t.TVAttribute)
                   .HasForeignKey<Product>(t => t.TVAttributeId)
                   .OnDelete(DeleteBehavior.SetNull)
                   .IsRequired(false);
        }
    }
}
 