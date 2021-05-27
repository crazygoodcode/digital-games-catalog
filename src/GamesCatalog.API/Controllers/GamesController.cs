using GamesCatalog.Core.Data;
using GamesCatalog.Core.Integrations;
using GamesCatalog.Core.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace GamesCatalog.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class GamesController : ControllerBase
    {
        private readonly IGameDataAPIService _gameDataAPIService;

        public GamesController(IGameDataAPIService gameDataAPIService)
        {
            _gameDataAPIService = gameDataAPIService;
        }

        [HttpGet]
        public Task<IEnumerable<Game>> GetAsync([FromQuery] string q, [FromQuery] string sort = "-metacritic", CancellationToken cancellationToken = default)
        {
            return _gameDataAPIService.SearchAsync(new GameSearchParams(q, sort), cancellationToken);
        }
    }
}
