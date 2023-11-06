using Swapy.Common.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Swapy.DAL.Configurations
{
    public class MemoryModelConfiguration : IEntityTypeConfiguration<MemoryModel>
    {
        public void Configure(EntityTypeBuilder<MemoryModel> builder)
        {
            builder.ToTable("MemoriesModels");
            builder.HasKey(m => m.Id);

            builder.Property(m => m.Id).IsRequired();

            builder.HasOne(m => m.Memory)
                   .WithMany(m => m.MemoriesModels)
                   .HasForeignKey(m => m.MemoryId)
                   .IsRequired();

            builder.HasOne(m => m.Model)
                   .WithMany(m => m.MemoriesModels)
                   .HasForeignKey(m => m.ModelId)
                   .IsRequired();

            builder.HasMany(m => m.ElectronicAttributes)
                   .WithOne(e => e.MemoryModel)
                   .IsRequired(false);
        }
    }
}
