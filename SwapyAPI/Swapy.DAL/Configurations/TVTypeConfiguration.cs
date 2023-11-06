using Swapy.Common.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Swapy.DAL.Configurations
{
    public class TVTypeConfiguration : IEntityTypeConfiguration<TVType>
    {
        public void Configure(EntityTypeBuilder<TVType> builder)
        {
            builder.ToTable("TVTypes");
            builder.HasKey(t => t.Id);

            builder.Property(t => t.Id).IsRequired();

            builder.Property(t => t.Name)
                   .HasColumnType("NVARCHAR(128)")
                   .HasMaxLength(128)
                   .IsRequired();

            builder.HasMany(t => t.TVAttributes)
                   .WithOne(t => t.TVType)
                   .HasForeignKey(t => t.TVTypeId)
                   .IsRequired(false);
        }
    }
}
