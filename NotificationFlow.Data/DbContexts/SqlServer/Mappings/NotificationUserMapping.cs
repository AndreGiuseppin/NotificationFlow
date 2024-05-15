using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NotificationFlow.Business.Entity;

namespace NotificationFlow.Data.DbContexts.SqlServer.Mappings
{
    public class NotificationUserMapping : IEntityTypeConfiguration<NotificationUser>
    {
        public void Configure(EntityTypeBuilder<NotificationUser> builder)
        {
            builder.ToTable(nameof(NotificationUser));

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Read)
                .IsRequired()
                .HasColumnType("bit")
                .HasDefaultValue(0);

            builder.HasOne(x => x.Users)
                .WithMany(x => x.NotificationUsers)
                .HasForeignKey(x => x.UserId);

            builder.HasOne(x => x.Notification)
                .WithMany(x => x.NotificationUsers)
                .HasForeignKey(x => x.NotificationId);
        }
    }
}
