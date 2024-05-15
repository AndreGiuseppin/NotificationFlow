using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NotificationFlow.Business.Entity;

namespace NotificationFlow.Data.DbContexts.SqlServer.Mappings
{
    public class ContactMapping : IEntityTypeConfiguration<Contact>
    {
        public void Configure(EntityTypeBuilder<Contact> builder)
        {
            builder.ToTable(nameof(Contact));

            builder.HasKey(x => x.Id);

            builder.Property(x => x.ContactType)
                .IsRequired();

            builder.Property(x => x.Value)
                .IsRequired()
                .HasColumnType("varchar(255)");

            builder.HasOne(x => x.User)
                .WithMany(x => x.Contacts)
                .HasForeignKey(x => x.UserId);
        }
    }
}
