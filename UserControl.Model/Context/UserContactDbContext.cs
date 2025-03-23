using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using UserControl.Model.Configurations;
using UserControl.Model.Entities;

namespace UserControl.Model.Context;

public class UserContactDbContext : DbContext
{

    public UserContactDbContext(DbContextOptions options) : base(options)
    {

    }

    public DbSet<UserEntity> Users => Set<UserEntity>();
    public DbSet<PhoneEntity> Phones => Set<PhoneEntity>();


    private void SetAuditEntities()
    {
        foreach (var entry in ChangeTracker.Entries<IBaseEntity>())
        {
            switch (entry.State)
            {
                case EntityState.Added:
                    entry.Entity.Created = DateTimeOffset.UtcNow;
                    break;

                case EntityState.Modified:
                    entry.Property(x => x.Created).IsModified = false;
                    entry.Entity.Modified = DateTimeOffset.UtcNow;
                    break;

                default:
                    break;
            }
        }
    }

    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        SetAuditEntities();
        return await base.SaveChangesAsync(cancellationToken);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfiguration(new UserEntityConfiguration());
        modelBuilder.ApplyConfiguration(new PhoneEntityConfiguration());
    }
}

