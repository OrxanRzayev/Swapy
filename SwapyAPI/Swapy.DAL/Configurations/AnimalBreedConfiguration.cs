using Swapy.Common.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Swapy.DAL.Configurations
{
    public class AnimalBreedConfiguration : IEntityTypeConfiguration<AnimalBreed>
    {
        public void Configure(EntityTypeBuilder<AnimalBreed> builder)
        {
            builder.ToTable("AnimalBreedsTypes");
            builder.HasKey(a => a.Id);

            builder.Property(a => a.Id).IsRequired();

            builder.Property(a => a.Name)
                   .HasColumnType("NVARCHAR(128)")
                   .HasMaxLength(128)
                   .IsRequired();

            builder.HasOne(a => a.AnimalType)
                   .WithMany(s => s.AnimalBreeds)
                   .HasForeignKey(a => a.AnimalTypeId)
                   .IsRequired();

            builder.HasMany(a => a.AnimalAttributes)
                   .WithOne(a => a.AnimalBreed)
                   .IsRequired(false);
        }
    }
}
