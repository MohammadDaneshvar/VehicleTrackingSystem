using System.Threading;
using System.Threading.Tasks;

namespace Framework.Data.EF
{
    public interface IUnitOfWork
    {
        Task BeginAsync(CancellationToken cancellationToken = default);
        void Commit();
        Task RollbackAsync(CancellationToken cancellationToken = default);
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
        //void Task BeginAsync();
        //void Task CommitAsync();
        //void Task RollbackAsync();
        //Task<int> SaveChangesAsync();
    }
}