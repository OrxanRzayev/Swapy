using Swapy.Common.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Swapy.DAL.Configurations
{
    public class FuelTypeConfiguration : IEntityTypeConfiguration<FuelType>
    {
        public void Configure(EntityTypeBuilder<FuelType> builder)
        {
            builder.ToTable("FuelTypes");
            builder.HasKey(f => f.Id);

            builder.Property(f => f.Id).IsRequired();

            builder.Property(f => f.Name)
                   .HasColumnType("NVARCHAR(128)")
                   .HasMaxLength(128)
                   .IsRequired();

            builder.HasMany(f => f.AutoAttributes)
                   .WithOne(f => f.FuelType)
                   .HasForeignKey(f => f.FuelTypeId)
                   .IsRequired(false);
        }
    }
}
