using Swapy.Common.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Swapy.DAL.Configurations
{
    public class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.ToTable("Products");
            builder.HasKey(p => p.Id);

            builder.Property(p => p.Id).IsRequired();

            builder.Property(p => p.Title)
                   .HasColumnType("NVARCHAR(128)")
                   .HasMaxLength(128)
                   .IsRequired();

            builder.Property(p => p.Description)
                   .HasColumnType("NVARCHAR(500)")
                   .HasMaxLength(500)
                   .IsRequired(false);

            builder.Property(p => p.Price)
                   .HasColumnType("Decimal(10,2)")
                   .HasDefaultValue(0)
                   .IsRequired();

            builder.Property(p => p.Views)
                   .HasColumnType("INT")
                   .HasDefaultValue(0)
                   .IsRequired();

            builder.Property(p => p.DateTime)
                   .HasColumnType("DATETIME")
                   .HasDefaultValueSql("GETDATE()")                                                                     
                   .IsRequired();

            builder.Property(p => p.IsDisable)
                   .HasColumnType("BIT")
                   .IsRequired();

            builder.HasOne(p => p.City)
                   .WithMany(c => c.Products)
                   .HasForeignKey(p => p.CityId)
                   .OnDelete(DeleteBehavior.Cascade)
                   .IsRequired();

            builder.HasOne(p => p.Currency)
                   .WithMany(c => c.Products)
                   .HasForeignKey(p => p.CurrencyId)
                   .IsRequired();

            builder.HasOne(p => p.User)
                   .WithMany(u => u.Products)
                   .HasForeignKey(p => p.UserId)
                   .IsRequired();

            builder.HasMany(p => p.Chats)
                   .WithOne(c => c.Product)
                   .HasForeignKey(c => c.ProductId)
                   .IsRequired(false);

            builder.HasMany(p => p.Images)
                   .WithOne(p => p.Product)
                   .HasForeignKey(p => p.ProductId)
                   .IsRequired(false);

            builder.HasOne(p => p.Subcategory)
                   .WithMany(p => p.Products)
                   .HasForeignKey(p => p.SubcategoryId)
                   .IsRequired();

            builder.HasOne(p => p.Category)
                   .WithMany(p => p.Products)
                   .HasForeignKey(p => p.CategoryId)
                   .IsRequired();

            builder.HasOne(p => p.AutoAttribute)
                   .WithOne(a => a.Product)
                   .HasForeignKey<AutoAttribute>(s => s.ProductId)
                   .OnDelete(DeleteBehavior.Cascade)
                   .IsRequired(false);

            builder.HasOne(p => p.ElectronicAttribute)
                   .WithOne(e => e.Product)
                   .HasForeignKey<ElectronicAttribute>(s => s.ProductId)
                   .OnDelete(DeleteBehavior.Cascade)
                   .IsRequired(false);

            builder.HasOne(p => p.ItemAttribute)
                   .WithOne(i => i.Product)
                   .HasForeignKey<ItemAttribute>(s => s.ProductId)
                   .OnDelete(DeleteBehavior.Cascade)
                   .IsRequired(false);

            builder.HasOne(p => p.AnimalAttribute)
                   .WithOne(a => a.Product)
                   .HasForeignKey<AnimalAttribute>(s => s.ProductId)
                   .OnDelete(DeleteBehavior.Cascade)
                   .IsRequired(false);

            builder.HasOne(p => p.TVAttribute)
                   .WithOne(t => t.Product)
                   .HasForeignKey<TVAttribute>(s => s.ProductId)
                   .OnDelete(DeleteBehavior.Cascade)
                   .IsRequired(false);

            builder.HasOne(p => p.RealEstateAttribute)
                   .WithOne(r => r.Product)
                   .HasForeignKey<RealEstateAttribute>(s => s.ProductId)
                   .OnDelete(DeleteBehavior.Cascade)
                   .IsRequired(false);

            builder.HasMany(p => p.FavoriteProducts)
                   .WithOne(f => f.Product)
                   .HasForeignKey(f => f.ProductId)
                   .OnDelete(DeleteBehavior.Cascade)
                   .IsRequired(false);
        }
    }
}
