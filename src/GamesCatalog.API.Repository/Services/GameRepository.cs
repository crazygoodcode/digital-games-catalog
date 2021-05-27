using AutoMapper;
using GamesCatalog.Core.Data;
using GamesCatalog.Core.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace GamesCatalog.API.Repository.Services
{
    public sealed class GameRepository : IGameRepository
    {
        private readonly GameCatalogContext _context;
        private readonly IMapper _mapper;

        public GameRepository(GameCatalogContext context,
            IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<long> AddAsync(Game game, CancellationToken cancellationToken = default)
        {
            Entities.Game entityGame = await _context.Games.FirstOrDefaultAsync(f => f.ExternalId == game.GameId);
            if (entityGame != null && entityGame.GameId > 0)
            {
                _mapper.Map<Entities.Game>(game);
            }
            else
            {
                entityGame = _mapper.Map<Entities.Game>(game);

                await _context.Games.AddAsync(entityGame);
            }

            await _context.SaveChangesAsync(cancellationToken);

            return entityGame.GameId;
        }

        public Task DeleteAsync(long id, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public Task<Game> GetAsync(long Id, CancellationToken cancellationToken = default)
        {
            var gameEntity = _context.Games.Include(i => i.Users).Where(w => w.GameId == Id).FirstOrDefaultAsync(cancellationToken);

            return null;
        }

        public Task<Game> GetByExternalIdAsync(long externalId, CancellationToken cancellationToken = default)
        {
            var gameEntity = _context.Games.Include(i => i.Users).Where(w => w.ExternalId == externalId).FirstOrDefaultAsync(cancellationToken);

            return null;
        }
    }
}
