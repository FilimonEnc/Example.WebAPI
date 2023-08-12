using Example.Application.Interfaces;
using Example.Core.Entities;
using Example.Infrastructure.Exceptions;

using Microsoft.EntityFrameworkCore;

using System;
using System.Linq;

namespace Example.Infrastructure
{
    public class CrmContext : DbContext, ICrmContext
    {
        public DbSet<ExampleEntity> Examples { get; set; } = null!;

        public DbSet<User> Users { get; set; } = null!;

        public CrmContext(DbContextOptions<CrmContext> options, bool migrate = true) : base(options)
        {
            if (migrate)
                Database.Migrate();
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfigurationsFromAssembly(this.GetType().Assembly);
            base.OnModelCreating(builder);
        }

        public override int SaveChanges()
        {
            var entries = ChangeTracker
                .Entries()
                .Where(e => e.Entity is BaseEntity && (
                        e.State == EntityState.Added
                        || e.State == EntityState.Modified));

            foreach (var entityEntry in entries)
            {
                ((BaseEntity)entityEntry.Entity).UpdatedDate = DateTime.Now;

                if (entityEntry.State == EntityState.Added)
                {
                    ((BaseEntity)entityEntry.Entity).CreatedDate = DateTime.Now;
                }
            }

            try
            {
                return base.SaveChanges();
            }
            catch (DbUpdateException ex)
            {
                throw new InfrastructureException($"При сохранении в базу данных возникла ошибка. {ex.Message}");
            }

        }

        public override System.Threading.Tasks.Task<int> SaveChangesAsync(System.Threading.CancellationToken cancellationToken = default)
        {
            var entries = ChangeTracker
                .Entries()
                .Where(e => e.Entity is BaseEntity && (
                        e.State == EntityState.Added
                        || e.State == EntityState.Modified));

            foreach (var entityEntry in entries)
            {
                ((BaseEntity)entityEntry.Entity).UpdatedDate = DateTime.Now;

                if (entityEntry.State == EntityState.Added)
                {
                    ((BaseEntity)entityEntry.Entity).CreatedDate = DateTime.Now;
                }
            }
            try
            {
                return base.SaveChangesAsync(cancellationToken);
            }
            catch (DbUpdateConcurrencyException ex)
            {
                throw new InfrastructureException($"При сохранении в базу данных возникла ошибка. {ex.Message}");
            }
            catch (DbUpdateException ex)
            {
                throw new InfrastructureException($"При сохранении в базу данных возникла ошибка. {ex.Message}");
            }
            catch (OperationCanceledException ex)
            {
                throw new InfrastructureException($"При сохранении в базу данных возникла ошибка. {ex.Message}");
            }
        }


    }
}
