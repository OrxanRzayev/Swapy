using Swapy.Common.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Swapy.DAL.Configurations
{
    public class ChatConfiguration : IEntityTypeConfiguration<Chat>
    {
        public void Configure(EntityTypeBuilder<Chat> builder)
        {
            builder.ToTable("Chats");
            builder.HasKey(c => c.Id);

            builder.Property(c => c.Id).IsRequired();

            builder.HasOne(c => c.Product)
                   .WithMany(p => p.Chats)
                   .HasForeignKey(c => c.ProductId)
                   .IsRequired();

            builder.HasOne(c => c.Buyer)
                   .WithMany(u => u.ChatsAsBuyer)
                   .HasForeignKey(c => c.BuyerId)
                   .IsRequired();

            builder.Property(c => c.IsReaded)
                   .IsRequired()
                   .HasColumnType("BIT");

            builder.HasMany(c => c.Messages)
                   .WithOne(m => m.Chat)
                   .HasForeignKey(m => m.ChatId)
                   .OnDelete(DeleteBehavior.SetNull)
                   .IsRequired(false);
        }
    }
}
