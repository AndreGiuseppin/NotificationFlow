using Microsoft.EntityFrameworkCore;
using NotificationFlow.Business.Entity;
using NotificationFlow.Data.DbContexts.SqlServer.Extensions;

namespace NotificationFlow.Data.Database.SqlServer
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<User> User { get; set; }
        public DbSet<Contact> Contact { get; set; }
        public DbSet<Notification> Notification { get; set; }
        public DbSet<NotificationUser> NotificationUser { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);
            modelBuilder.ApplyGlobalFilters<ISoftDeleteEntity>(e => e.DeletedAt == null);

            base.OnModelCreating(modelBuilder);
        }

        public override Task<int> SaveChangesAsync(
            CancellationToken cancellationToken = default)
        {
            UpdateAuditDates();

            return base.SaveChangesAsync(cancellationToken);
        }

        private void UpdateAuditDates()
        {
            foreach (var entry in ChangeTracker.Entries())
            {
                switch (entry.State)
                {
                    case EntityState.Added:
                        entry.CurrentValues[nameof(Entity.CreatedAt)] = DateTime.UtcNow;
                        break;
                    case EntityState.Modified:
                        entry.CurrentValues[nameof(Entity.UpdatedAt)] = DateTime.UtcNow;
                        break;
                    case EntityState.Deleted:
                        entry.State = EntityState.Modified;
                        entry.CurrentValues[nameof(Entity.DeletedAt)] = DateTime.UtcNow;
                        break;
                }
            }
        }
    }
}
