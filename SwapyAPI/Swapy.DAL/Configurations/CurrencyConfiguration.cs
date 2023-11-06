using Swapy.Common.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Swapy.DAL.Configurations
{
    public class CurrencyConfiguration : IEntityTypeConfiguration<Currency>
    {
        public void Configure(EntityTypeBuilder<Currency> builder)
        {
            builder.ToTable("Currencies");
            builder.HasKey(c => c.Id);

            builder.Property(c => c.Id).IsRequired();

            builder.HasMany(c => c.Products)
                   .WithOne(p => p.Currency)
                   .HasForeignKey(p => p.CurrencyId)
                   .IsRequired(false);
        }
    }
}
