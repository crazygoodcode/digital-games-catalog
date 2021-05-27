using GamesCatalog.Core.Models;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace GamesCatalog.Core.Integrations
{
    public interface IGameDataAPIService
    {
        Task<IEnumerable<Game>> SearchAsync(GameSearchParams query, CancellationToken cancellationToken = default);
        Task<Game> GetAsync(long gameId, CancellationToken cancellationToken = default);
    }
}
