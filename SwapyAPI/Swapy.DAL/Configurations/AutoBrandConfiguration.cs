using Swapy.Common.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Swapy.DAL.Configurations
{
    public class AutoBrandConfiguration : IEntityTypeConfiguration<AutoBrand>
    {
        public void Configure(EntityTypeBuilder<AutoBrand> builder)
        {
            builder.ToTable("AutoBrands");
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id).IsRequired();

            builder.Property(x => x.Name)
                   .IsRequired()
                   .HasMaxLength(32);

            builder.HasMany(x => x.AutoModels)
                   .WithOne(x => x.AutoBrand)
                   .IsRequired(false);
        }
    }
}
