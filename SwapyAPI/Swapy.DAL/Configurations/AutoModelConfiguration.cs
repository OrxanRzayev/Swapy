using Swapy.Common.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Swapy.DAL.Configurations
{
    public class AutoModelConfiguration : IEntityTypeConfiguration<AutoModel>
    {
        public void Configure(EntityTypeBuilder<AutoModel> builder)
        {
            builder.ToTable("AutoModels");
            builder.HasKey(a => a.Id);

            builder.Property(a => a.Id).IsRequired();

            builder.Property(a => a.Name).IsRequired();

            builder.HasOne(a => a.AutoBrand)
                   .WithMany(a => a.AutoModels)
                   .HasForeignKey(a => a.AutoBrandId)
                   .IsRequired();

            builder.HasOne(a => a.AutoType)
                   .WithMany(s => s.AutoModels)
                   .HasForeignKey(s => s.AutoTypeId)
                   .IsRequired();

            builder.HasMany(a => a.AutoAttributes)
                   .WithOne(a => a.AutoModel)
                   .IsRequired(false);
        }
    }
}
