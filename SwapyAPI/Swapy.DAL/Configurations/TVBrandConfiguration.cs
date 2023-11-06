using Swapy.Common.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Swapy.DAL.Configurations
{
    public class TVBrandConfiguration : IEntityTypeConfiguration<TVBrand>
    {
        public void Configure(EntityTypeBuilder<TVBrand> builder)
        {
            builder.ToTable("TVBrands");
            builder.HasKey(t => t.Id);

            builder.Property(t => t.Id).IsRequired();

            builder.Property(t => t.Name)
                   .IsRequired()
                   .HasMaxLength(32);

            builder.HasMany(t => t.TVAttributes)
                   .WithOne(t => t.TVBrand)
                   .HasForeignKey(t => t.TVBrandId)
                   .IsRequired(false);
        }
    }
}
