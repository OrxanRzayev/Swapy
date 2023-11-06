using Swapy.Common.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Swapy.DAL.Configurations
{
    public class ModelColorConfiguration : IEntityTypeConfiguration<ModelColor>
    {
        public void Configure(EntityTypeBuilder<ModelColor> builder)
        {
            builder.ToTable("ModelsColors");
            builder.HasKey(m => m.Id);

            builder.Property(m => m.Id).IsRequired();

            builder.HasOne(m => m.Model)
                   .WithMany(m => m.ModelsColors)
                   .HasForeignKey(m => m.ModelId)
                   .IsRequired();

            builder.HasOne(m => m.Color)
                   .WithMany(c => c.ModelsColors)
                   .HasForeignKey(m => m.ColorId)
                   .IsRequired();

            builder.HasMany(m => m.ElectronicAttributes)
                   .WithOne(e => e.ModelColor)
                   .IsRequired(false);
        }
    }
}
