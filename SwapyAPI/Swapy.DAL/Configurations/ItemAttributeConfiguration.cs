using Swapy.Common.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Swapy.DAL.Configurations
{
    public class ItemAttributeConfiguration : IEntityTypeConfiguration<ItemAttribute>
    {
        public void Configure(EntityTypeBuilder<ItemAttribute> builder)
        {
            builder.ToTable("ItemAttributes");
            builder.HasKey(i => i.Id);

            builder.Property(i => i.Id).IsRequired();

            builder.Property(i => i.IsNew)
                   .IsRequired()
                   .HasColumnType("BIT");

            builder.HasOne(i => i.ItemType)
                   .WithMany(s => s.ItemAttributes)
                   .HasForeignKey(i => i.ItemTypeId)
                   .IsRequired();

            builder.HasOne(i => i.Product)
                   .WithOne(p => p.ItemAttribute)
                   .HasForeignKey<Product>(p => p.ItemAttributeId)
                   .OnDelete(DeleteBehavior.SetNull)
                   .IsRequired(false);
        }
    }
}