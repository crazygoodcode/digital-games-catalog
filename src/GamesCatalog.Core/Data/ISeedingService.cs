using System.Threading;
using System.Threading.Tasks;

namespace GamesCatalog.Core.Data
{
    public interface ISeedingService
    {
        Task SeedAsync(CancellationToken cancellationToken = default);
    }
}
