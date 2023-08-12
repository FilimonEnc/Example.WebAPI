using Example.Core.Entities;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

using System.Threading;
using System.Threading.Tasks;

namespace Example.Application.Interfaces
{
    public interface ICrmContext
    {

        DbSet<ExampleEntity> Examples { get; set; }

        DbSet<User> Users { get; set; }


        DbSet<TEntity> Set<TEntity>() where TEntity : class;

        public int SaveChanges();
        Task<int> SaveChangesAsync(CancellationToken cancellationToken);

        EntityEntry<TEntity> Entry<TEntity>(TEntity entity) where TEntity : class;
    }
}
