using GamesCatalog.API.Repository;
using GamesCatalog.Core.Data;
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
        private readonly ISeedingService _seedingService;

        public SeedController(ISeedingService seedingService)
        {
            _seedingService = seedingService;
        }

        [HttpPost]
        public Task SeedAsync(CancellationToken cancellationToken = default)
        {
            return _seedingService.SeedAsync(cancellationToken);
        }
    }
}
