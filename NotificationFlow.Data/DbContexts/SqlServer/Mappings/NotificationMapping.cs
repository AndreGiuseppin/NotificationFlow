using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NotificationFlow.Business.Entity;

namespace NotificationFlow.Data.DbContexts.SqlServer.Mappings
{
    public class NotificationMapping : IEntityTypeConfiguration<Notification>
    {
        public void Configure(EntityTypeBuilder<Notification> builder)
        {
            builder.ToTable(nameof(Notification));

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Title)
                .IsRequired()
                .HasColumnType("varchar(255)");

            builder.Property(x => x.Description)
                .IsRequired()
                .HasColumnType("varchar(255)");

            builder.Property(x => x.IsGeneral)
                .IsRequired()
                .HasColumnType("bit")
                .HasDefaultValue(0);
        }
    }
}
