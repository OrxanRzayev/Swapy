using Swapy.Common.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Swapy.DAL.Configurations
{
    public class ElectronicBrandTypeConfiguration : IEntityTypeConfiguration<ElectronicBrandType>
    {
        public void Configure(EntityTypeBuilder<ElectronicBrandType> builder)
        {
            builder.ToTable("ElectronicBrandsTypes");
            builder.HasKey(e => e.Id);

            builder.Property(e => e.Id).IsRequired();

            builder.HasOne(e => e.ElectronicBrand)
                   .WithMany(e => e.ElectronicBrandsTypes)
                   .HasForeignKey(e => e.ElectronicBrandId)
                   .OnDelete(DeleteBehavior.Cascade)
                   .IsRequired();

            builder.HasOne(e => e.ElectronicType)
                   .WithMany(s => s.ElectronicBrandsTypes)
                   .HasForeignKey(e => e.ElectronicTypeId)
                   .IsRequired();

            builder.HasMany(x => x.Models)
                   .WithOne(x => x.ElectronicBrandType)
                   .IsRequired(false);
        }
    }
}
