using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NotificationFlow.Business.Entity;

namespace NotificationFlow.Data.DbContexts.SqlServer.Mappings
{
    public class NotificationPreferenceMapping : IEntityTypeConfiguration<NotificationPreference>
    {
        public void Configure(EntityTypeBuilder<NotificationPreference> builder)
        {
            builder.ToTable(nameof(NotificationPreference));

            builder.HasKey(x => x.Id);

            builder.Property(x => x.ReceiveGeneralNotifications)
                .IsRequired()
                .HasColumnType("bit")
                .HasDefaultValue(0);

            builder.Property(x => x.ReceiveSpecificNotifications)
                .IsRequired()
                .HasColumnType("bit")
                .HasDefaultValue(0);

            builder.HasOne(x => x.User)
                .WithOne(x => x.NotificationPreference)
                .HasForeignKey<NotificationPreference>(x => x.UserId);
        }
    }
}
