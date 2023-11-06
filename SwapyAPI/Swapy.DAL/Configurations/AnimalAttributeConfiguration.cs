using Swapy.Common.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Swapy.DAL.Configurations
{
    public class AnimalAttributeConfiguration : IEntityTypeConfiguration<AnimalAttribute>
    {
        public void Configure(EntityTypeBuilder<AnimalAttribute> builder)
        {
            builder.ToTable("AnimalAttributes"); 
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id).IsRequired();
                
            builder.HasOne(x => x.AnimalBreed)
                   .WithMany(x => x.AnimalAttributes)
                   .HasForeignKey(p => p.AnimalBreedId)
                   .IsRequired();

            builder.HasOne(x => x.Product)
                   .WithOne(x => x.AnimalAttribute)
                   .HasForeignKey<Product>(x => x.AnimalAttributeId)
                   .OnDelete(DeleteBehavior.SetNull)
                   .IsRequired(false);
        }
    }
}
