using GamesCatalog.API.Repository;
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
    public class SeedController : ControllerBase
    {
        private readonly GameCatalogContext _context;

        public SeedController(GameCatalogContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task SeedAsync(CancellationToken cancellationToken = default)
        {
            if (cancellationToken.IsCancellationRequested) return;

            var user1 = _context.Users.FirstOrDefault(f => f.Username.Equals("gamer1"));
            if (user1 == null)
            {
                await _context.Users.AddAsync(new Repository.Entities.User
                {
                    Username = "gamer1",
                    Email = "gamer1@game.net",
                    Verified = true
                });
            }

            var user2 = _context.Users.FirstOrDefault(f => f.Username.Equals("gamer2"));
            if (user2 == null)
            {
                await _context.Users.AddAsync(new Repository.Entities.User
                {
                    Username = "gamer2",
                    Email = "gamer2@game.net",
                    Verified = true
                });
            }

            await _context.SaveChangesAsync(cancellationToken);
        }
    }
}
