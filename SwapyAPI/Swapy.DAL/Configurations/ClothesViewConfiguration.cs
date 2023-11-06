using Swapy.Common.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Swapy.DAL.Configurations
{
    public class ClothesViewConfiguration : IEntityTypeConfiguration<ClothesView>
    {
        public void Configure(EntityTypeBuilder<ClothesView> builder)
        {
            builder.ToTable("ClothesViews");
            builder.HasKey(s => s.Id);

            builder.Property(s => s.Id).IsRequired();

            builder.Property(s => s.Name)
                   .HasColumnType("NVARCHAR(128)")
                   .HasMaxLength(128)
                   .IsRequired();

            builder.Property(x => x.IsChild)
                   .IsRequired()
                   .HasColumnType("BIT");  
              
            builder.HasOne(s => s.Gender)
                   .WithMany(s => s.ClothesViews) 
                   .HasForeignKey(s => s.GenderId) 
                   .IsRequired(); 

            builder.HasOne(s => s.ClothesType)  
                   .WithMany(c => c.ClothesViews) 
                   .HasForeignKey(s => s.ClothesTypeId)
                   .IsRequired();

            builder.HasMany(s => s.ClothesBrandViews)
                  .WithOne(c => c.ClothesView)
                  .HasForeignKey(s => s.ClothesViewId)
                  .IsRequired(false);  
        }
    } 
}     
