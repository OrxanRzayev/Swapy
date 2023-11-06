using Swapy.Common.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Swapy.DAL.Configurations
{
    public class ElectronicAttributeConfiguration : IEntityTypeConfiguration<ElectronicAttribute>
    {
        public void Configure(EntityTypeBuilder<ElectronicAttribute> builder)
        {
            builder.ToTable("ElectronicAttributes");
            builder.HasKey(e => e.Id);

            builder.Property(e => e.Id).IsRequired();

            builder.Property(e => e.IsNew)
                   .IsRequired()
                   .HasColumnType("BIT");

            builder.HasOne(e => e.MemoryModel)
                   .WithMany(m => m.ElectronicAttributes)
                   .HasForeignKey(e => e.MemoryModelId)
                   .IsRequired();

            builder.HasOne(e => e.ModelColor)
                   .WithMany(m => m.ElectronicAttributes)
                   .HasForeignKey(e => e.ModelColorId)
                   .IsRequired();

            builder.HasOne(e => e.Product)
                   .WithOne(p => p.ElectronicAttribute)
                   .HasForeignKey<Product>(p => p.ElectronicAttributeId)
                   .OnDelete(DeleteBehavior.SetNull)
                   .IsRequired(false);
        }
    }
}
