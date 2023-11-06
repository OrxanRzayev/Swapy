using Swapy.Common.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Swapy.DAL.Configurations
{
    public class ElectronicBrandConfiguration : IEntityTypeConfiguration<ElectronicBrand>
    {
        public void Configure(EntityTypeBuilder<ElectronicBrand> builder)
        {
            builder.ToTable("ElectronicBrands");
            builder.HasKey(e => e.Id);

            builder.Property(e => e.Id).IsRequired();

            builder.Property(e => e.Name)
                   .IsRequired()
                   .HasMaxLength(32);

            builder.HasMany(e => e.ElectronicBrandsTypes)
                   .WithOne(e => e.ElectronicBrand)
                   .IsRequired(false);
        }
    }
}

