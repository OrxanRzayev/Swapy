using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Swapy.Common.Entities;

namespace Swapy.DAL.Configurations
{
    public class UserSubscriptionConfiguration : IEntityTypeConfiguration<UserSubscription>
    {
        public void Configure(EntityTypeBuilder<UserSubscription> builder)
        {
            builder.ToTable("UsersSubscriptions");
            builder.HasKey(us => us.Id);

            builder.Property(us => us.Id).IsRequired();

            builder.HasOne(us => us.Subscription)
                   .WithOne(s => s.UserSubscription)
                   .HasForeignKey<Subscription>(s => s.UserSubscriptionId)
                   .IsRequired();

            builder.HasOne(us => us.Recipient)
                   .WithMany(s => s.SubscriptionsRecipient)
                   .HasForeignKey(us => us.RecipientId)
                   .OnDelete(DeleteBehavior.SetNull)
                   .IsRequired(false);
        }
    }
}
