using AutoMapper;
using GamesCatalog.Core.Data;
using GamesCatalog.Core.Exceptions;
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
    public sealed class UserRepository : IUserRepository
    {
        private readonly GameCatalogContext _context;
        private readonly IGameRepository _gameRepository;
        private readonly IMapper _mapper;

        public UserRepository(GameCatalogContext context,
            IGameRepository gameRepository,
            IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
            _gameRepository = gameRepository;
        }

        public async Task<long> AddAsync(User user, CancellationToken cancellationToken = default)
        {
            var entity = _mapper.Map<Entities.User>(user);

            if (entity != null && entity.UserId < 1)
            {
                _context.Add<Entities.User>(entity);
                
                await _context.SaveChangesAsync();

                user = _mapper.Map<User>(entity);
            }

            return entity.UserId;
        }

        public async Task<long> AddGameToUserAsync(long userId, Game game, CancellationToken cancellationToken = default)
        {
            var entity = await _context.Users
                .Include(i => i.Games)
                .Where(w => w.UserId == userId)
                .FirstOrDefaultAsync(cancellationToken);

            if (entity == null) throw new GameCatalogInvalidUserException();

            await _gameRepository.AddAsync(game, cancellationToken);

            if (!entity.Games.Any(a => a.ExternalId == game.GameId))
            {
                var entityGame = await _context.Games.FirstOrDefaultAsync(f => f.ExternalId == game.GameId);
                entity.Games.Add(entityGame);
                await _context.SaveChangesAsync(cancellationToken);
            }

            return game.GameId;
        }

        public async Task DeleteAsync(long id, CancellationToken cancellationToken = default)
        {
            var result = await _context.Users.FindAsync(id, cancellationToken);

            if (result != null)
            {
                _context.Remove(result);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<User> GetAsync(long Id, CancellationToken cancellationToken = default)
        {
            var entity = await _context.Users
                .Include(i=> i.Games)
                .Where(w=> w.UserId == Id)
                .FirstOrDefaultAsync(cancellationToken);

            if (entity == null) return null;

            return _mapper.Map<User>(entity);
        }

        public async Task<User> GetByEmailAsync(string email, CancellationToken cancellationToken = default)
        {
            var entity = await _context.Users
                .Include(i=>i.Games)
                .Where(w=> w.Email.Equals(email, StringComparison.OrdinalIgnoreCase))
                .FirstOrDefaultAsync();

            if (entity == null) return null;

            return _mapper.Map<User>(entity);
        }

        public async Task<User> GetByUsernameAsync(string username, CancellationToken cancellationToken = default)
        {
            var entity = await _context.Users
             .Include(i => i.Games)
             .Where(w => w.Username.Equals(username, StringComparison.OrdinalIgnoreCase))
             .FirstOrDefaultAsync();

            if (entity == null) return null;

            return _mapper.Map<User>(entity);
        }
    }
}
