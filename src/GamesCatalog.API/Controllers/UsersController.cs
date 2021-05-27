using GamesCatalog.API.ViewModel;
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
    public class UsersController : ControllerBase
    {
        private readonly IUserRepository _userRepository;
        private readonly IGameDataAPIService _gameDataAPIService;

        public UsersController(IUserRepository userRepository,
            IGameDataAPIService gameDataAPIService)
        {
            _userRepository = userRepository;
            _gameDataAPIService = gameDataAPIService;
        }

       
        [HttpGet]
        [Route("{userId:long}", Name = "Get")]
        public async Task<ActionResult<User>> GetAsync(long userId, CancellationToken cancellationToken = default)
        {
            try
            {
                if (userId < 1) return BadRequest();

                var user = await _userRepository.GetAsync(userId, cancellationToken);

                if (user == null) return NotFound();

                return user;
            }
            catch (Exception ex)
            {
                // Log - prod system would not return ex.Message
                return StatusCode(500, ex.Message);
            }
        }


        [HttpPost]
        public async Task<ActionResult> AddAsync([FromBody] User user, CancellationToken cancellationToken = default)
        {
            if (user != null)
            {
                try
                {
                    user.UserId = await _userRepository.AddAsync(user, cancellationToken);

                    return CreatedAtRoute("Get", new { userId = user.UserId }, user);
                }
                catch (Exception ex)
                {
                    // Log - prod system would not return ex.Message
                    return StatusCode(500, ex.Message);
                }
            }

            return BadRequest();
        }

        [HttpPost]
        [Route("/users/{userId:long}/games")]
        public async Task<ActionResult> AddGameForUserAsync(long userId, [FromBody] Game game, CancellationToken cancellationToken = default)
        {
            if (game != null)
            {
                try
                {
                    var gameFromService = await _gameDataAPIService.GetAsync(game.GameId, cancellationToken);

                    if (gameFromService == null) return NotFound();

                    game.GameId = await _userRepository.AddGameToUserAsync(userId, gameFromService, cancellationToken);

                    return CreatedAtRoute("GetUserGame", new { gameId = game.GameId }, game);
                }
                catch (Exception ex)
                {
                    // Log - prod system would not return ex.Message
                    return StatusCode(500, ex.Message);
                }
            }

            return BadRequest();
        }

        [HttpGet]
        [Route("/users/{userId:long}/games/{gameId:long}", Name = "GetUserGame")]
        public async Task<ActionResult<Game>> GetAsync(long userId, long gameId, CancellationToken cancellationToken = default)
        {
            var user = await _userRepository.GetAsync(userId, cancellationToken);
            if (user == null) return NotFound();
            
            var game = user.Games.FirstOrDefault(f => f.GameId == gameId);
            if (game == null) return NotFound();

            return game;
        }

        [HttpPost("{userId:long}/comparison")]
        public async Task<ActionResult<ComparisonGet>> DeltaAsync(long userId, [FromBody] ComparisonPost model, CancellationToken cancellationToken = default)
        {
            if (model.IsValid())
            {
                var user1 = await _userRepository.GetAsync(userId, cancellationToken);
                if (user1 == null) return NotFound();

                var user2 = await _userRepository.GetAsync(model.OtherUserId, cancellationToken);
                if (user2 == null) return NotFound();

                switch (model.Comparison)
                {
                    case "union":
                        var unionResult = new Dictionary<long, Game>();
                        if ((user1.Games?.Count ?? 0) > 0)
                            foreach (var game in user1.Games)
                                unionResult.Add(game.GameId, game);
                        if ((user2.Games?.Count ?? 0) > 0)
                            foreach (var game in user2.Games)
                                if (!unionResult.ContainsKey(game.GameId))
                                    unionResult.Add(game.GameId, game);

                        return new ComparisonGet
                        {
                            UserId = userId,
                            OtherUserId = model.OtherUserId,
                            Comparison = model.Comparison,
                            Games = unionResult.Select(s=> s.Value).ToList()
                        };
                    case "intersection":
                        var intersection = from g in user1.Games
                                           join g2 in user2.Games on g.GameId equals g2.GameId
                                           select g;

                        return new ComparisonGet
                        {
                            UserId = userId,
                            OtherUserId = model.OtherUserId,
                            Comparison = model.Comparison,
                            Games = intersection.ToList()
                        };
                    case "difference":
                        if ((user1.Games?.Count ?? 0) == 0)
                            return new ComparisonGet
                            {
                                UserId = userId,
                                OtherUserId = model.OtherUserId,
                                Comparison = model.Comparison,
                                Games = user2.Games
                            };

                        var user1Games = user1.Games.Select(s => s.GameId).ToList();
                        var diff = user2.Games?.Where(w => !user1Games.Contains(w.GameId));

                        return new ComparisonGet
                        {
                            UserId = userId,
                            OtherUserId = model.OtherUserId,
                            Comparison = model.Comparison,
                            Games = diff?.ToList() ?? new List<Game>()
                        };
                }
            }
            
            return BadRequest();
        }
    }
}
