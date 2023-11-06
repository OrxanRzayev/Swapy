using Swapy.Common.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Swapy.DAL.Configurations
{
    public class GenderConfiguration : IEntityTypeConfiguration<Gender>
    { 
        public void Configure(EntityTypeBuilder<Gender> builder)
        {
            builder.ToTable("Genders");
            builder.HasKey(g => g.Id);
             
            builder.Property(g => g.Id).IsRequired();

            builder.Property(g => g.Name)
                   .HasColumnType("NVARCHAR(128)")
                   .HasMaxLength(128)
                   .IsRequired();

            builder.HasMany(g => g.ClothesViews)
                   .WithOne(c => c.Gender) 
                   .HasForeignKey(c => c.GenderId)
                   .IsRequired(false);  
        }
    }
}
