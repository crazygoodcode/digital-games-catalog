using GamesCatalog.API.Repository.Entities;
using GamesCatalog.Core.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace GamesCatalog.API.Repository.Services
{
    /// <summary>
    /// Simple static seeding data to showcase possible DI solutions
    /// </summary>
    public sealed class SeedingService : ISeedingService
    {
        private readonly GameCatalogContext _context;

        private const int ExternalId_GTA = 3498,
                          ExternalId_PayDay2 = 3939,
                          ExternalId_HalfLife2 = 13537,
                          ExternalId_Limbo = 1030;

        public SeedingService(GameCatalogContext context)
        {
            _context = context;
        }

        public async Task SeedAsync(CancellationToken cancellationToken = default)
        {
            // Users
            User user1 = _context.Users
                .Include(i=> i.Games)
                .FirstOrDefault(f => f.Username.Equals("gamer1"));
            if (user1 == null)
            {
                user1 = new User
                {
                    Username = "gamer1",
                    Email = "gamer1@game.net",
                    Verified = true
                };

                await _context.Users.AddAsync(user1);
            }

            User user2 = _context.Users
                .Include(i => i.Games)
                .FirstOrDefault(f => f.Username.Equals("gamer2"));
            if (user2 == null)
            {
                user2 = new User
                {
                    Username = "gamer2",
                    Email = "gamer2@game.net",
                    Verified = true
                };

                await _context.Users.AddAsync(user2);
            }

            // Games
            Game gta = _context.Games.FirstOrDefault(f => f.ExternalId == ExternalId_GTA);
            if (gta == null)
            {
                gta = new Game
                {
                    Name = "Grand Theft Auto V",
                    ExternalId = ExternalId_GTA,
                    Rating = 4.48f,
                    MetacriticScore = 97,
                    Released = "2013-09-17",
                    LastSync = DateTime.Parse("2021-03-03T20:31:29")
                };

                await _context.Games.AddAsync(gta);
            }

            Game payDay2 = _context.Games.FirstOrDefault(f => f.ExternalId == ExternalId_PayDay2);
            if (payDay2 == null)
            {
                payDay2 = new Game
                {
                    Name = "PAYDAY 2",
                    ExternalId = ExternalId_PayDay2,
                    Rating = 3.5f,
                    MetacriticScore = 79,
                    Released = "2013-08-13",
                    LastSync = DateTime.Parse("2019-11-05T16:34:08")
                };

                await _context.Games.AddAsync(payDay2);
            }

            Game limbo = _context.Games.FirstOrDefault(f => f.ExternalId == ExternalId_Limbo);
            if (limbo == null)
            {
                limbo = new Game
                {
                    Name = "Limbo",
                    ExternalId = ExternalId_Limbo,
                    Rating = 4.17f,
                    MetacriticScore = 88,
                    Released = "2010-07-21",
                    LastSync = DateTime.Parse("2021-04-03T11:10:51")
                };

                await _context.Games.AddAsync(limbo);
            }

            Game halfLife2 = _context.Games.FirstOrDefault(f => f.ExternalId == ExternalId_HalfLife2);
            if (halfLife2 == null)
            {
                halfLife2 = new Game
                {
                    Name = "Half-Life 2",
                    ExternalId = ExternalId_HalfLife2,
                    Rating = 4.5f,
                    MetacriticScore = 96,
                    Released = "2004-11-16",
                    LastSync = DateTime.Parse("2019-09-17T15:58:20")
                };

                await _context.Games.AddAsync(halfLife2);
            }

            await _context.SaveChangesAsync(cancellationToken);

            if ((user1.Games?.Count ?? 0) == 0)
            {
                user1.Games.AddRange(new List<Game>
                {
                    gta,
                    limbo,
                    halfLife2
                });
            }

            if ((user2.Games?.Count ?? 0) == 0)
            {
                user1.Games.AddRange(new List<Game>
                {
                    gta,
                    payDay2
                });
            }

            await _context.SaveChangesAsync(cancellationToken);
        }
    }
}
