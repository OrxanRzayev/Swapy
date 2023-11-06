using Swapy.Common.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Swapy.DAL.Configurations
{
    public class ScreenDiagonalConfiguration : IEntityTypeConfiguration<ScreenDiagonal>
    {
        public void Configure(EntityTypeBuilder<ScreenDiagonal> builder)
        {
            builder.ToTable("ScreenDiagonals");
            builder.HasKey(s => s.Id);

            builder.Property(s => s.Id).IsRequired();

            builder.Property(s => s.Diagonal)
                   .HasColumnType("INT")
                   .IsRequired();

            builder.HasMany(s => s.TVAttributes)
                   .WithOne(s => s.ScreenDiagonal)
                   .HasForeignKey(s => s.ScreenDiagonalId)
                   .IsRequired(false);

        }
    }
}
