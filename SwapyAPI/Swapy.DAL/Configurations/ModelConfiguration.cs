using Swapy.Common.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Swapy.DAL.Configurations
{
    public class ModelConfiguration : IEntityTypeConfiguration<Model>
    {
        public void Configure(EntityTypeBuilder<Model> builder)
        {
            builder.ToTable("Models");
            builder.HasKey(m => m.Id);

            builder.Property(m => m.Id).IsRequired();

            builder.Property(m => m.Name)
                   .IsRequired()
                   .HasMaxLength(256);

            builder.HasOne(m => m.ElectronicBrandType)
                   .WithMany(e => e.Models)
                   .HasForeignKey(m => m.ElectronicBrandTypeId)
                   .IsRequired();

            builder.HasMany(m => m.MemoriesModels)
                   .WithOne(m => m.Model)
                   .IsRequired();

            builder.HasMany(m => m.ModelsColors)
                   .WithOne(m => m.Model)
                   .IsRequired(false);
        }
    }
}
