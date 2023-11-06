using Swapy.Common.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Swapy.DAL.Configurations
{
    public class TransmissionTypeConfiguration : IEntityTypeConfiguration<TransmissionType>
    {
        public void Configure(EntityTypeBuilder<TransmissionType> builder)
        {
            builder.ToTable("TransmissionTypes");
            builder.HasKey(tt => tt.Id);

            builder.Property(tt => tt.Id).IsRequired();

            builder.Property(tt => tt.Name)
                   .HasColumnType("NVARCHAR(128)")
                   .HasMaxLength(128)
                   .IsRequired();

            builder.HasMany(tt => tt.AutoAttributes)
                   .WithOne(tt => tt.TransmissionType)
                   .HasForeignKey(tt => tt.TransmissionTypeId)
                   .IsRequired(false);
        }
    }
}
