using System.Threading;
using System.Threading.Tasks;

namespace Application.Common.Interfaces
{
    /// <summary>
    /// IApplicationDbContext interface
    /// </summary>
    public interface IApplicationDbContext
    {
        /// <summary>
        /// Persisting the changes
        /// </summary>
        /// <param name="cancellationToken">Cancellation token</param>
        /// <returns>An integer</returns>
        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}
