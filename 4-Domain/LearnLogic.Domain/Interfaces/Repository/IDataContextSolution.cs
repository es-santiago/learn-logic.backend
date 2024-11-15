using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System.Threading;
using System.Threading.Tasks;

namespace LearnLogic.Domain.Interfaces.Repository
{
    public interface IDataContextSolution
    {
        DbSet<TEntity> Set<TEntity>() where TEntity : class;
        int SaveChanges();
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
        EntityEntry Entry(object entity);
    }
}
