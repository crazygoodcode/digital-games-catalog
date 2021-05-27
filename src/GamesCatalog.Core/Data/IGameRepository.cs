using GamesCatalog.Core.Models;
using System.Threading;
using System.Threading.Tasks;

namespace GamesCatalog.Core.Data
{
    public interface IGameRepository
    {
        Task<Game> GetAsync(long Id, CancellationToken cancellationToken = default);
        Task<Game> GetByExternalIdAsync(long externalId, CancellationToken cancellationToken = default);
        Task<long> AddAsync(Game game, CancellationToken cancellationToken = default);
        Task DeleteAsync(long id, CancellationToken cancellationToken = default);
    }
}
