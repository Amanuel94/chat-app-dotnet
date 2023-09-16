using Microsoft.EntityFrameworkCore;
using ChatApp.Models.EntityModels;

namespace ChatApp.Models.Data
{
    public class ChatAppDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Message> Messages { get; set; }

        public ChatAppDbContext(DbContextOptions<ChatAppDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<User>(entity =>
            {
                entity.HasKey(u => u.Id);
                entity.Property(u => u.Email).IsRequired();
                entity.Property(u => u.Password).IsRequired();
                entity.Property(u => u.CreatedAt).IsRequired();
                entity.Property(u => u.ModifiedAt).IsRequired();
            });

            modelBuilder.Entity<Message>(entity =>
            {
                entity.HasKey(m => m.Id);
                entity.Property(m => m.Content).IsRequired();
                entity.Property(m => m.CreatedAt).IsRequired();
                entity.Property(m => m.ModifiedAt).IsRequired();
                entity.HasOne(m => m.User)
                .WithMany(u => u.Messages)
                .HasForeignKey(u => u.UserId)
                .OnDelete(DeleteBehavior.Restrict);
            });
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            foreach (var entry in ChangeTracker.Entries<BaseEntity>())
            {
                entry.Entity.ModifiedAt = DateTime.Now.ToUniversalTime();
                if (entry.State == EntityState.Added)
                {
                    entry.Entity.CreatedAt = DateTime.Now.ToUniversalTime();
                }
            }

            return base.SaveChangesAsync(cancellationToken);
        }

    }
}