using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace GamesCatalog.Core.Integrations
{
    public interface IGameDataAPIService
    {
        Task SearchAsync(CancellationToken cancellationToken = default);
    }
}
