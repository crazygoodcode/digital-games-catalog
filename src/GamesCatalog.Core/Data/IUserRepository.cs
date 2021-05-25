using GamesCatalog.Core.Models;
using System.Threading;
using System.Threading.Tasks;

namespace GamesCatalog.Core.Data
{
    public interface IUserRepository
    {
        Task<User> GetAsync(long Id, CancellationToken cancellationToken = default);
        Task<User> GetByUsernameAsync(string username, CancellationToken cancellationToken = default);
        Task<User> GetByEmailAsync(string email, CancellationToken cancellationToken = default);
        Task<long> AddAsync(User user, CancellationToken cancellationToken = default);
        Task DeleteAsync(long id, CancellationToken cancellationToken = default);
    }
}
