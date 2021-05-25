using GamesCatalog.API.Repository.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GamesCatalog.API.Controllers
{
    [ApiController]
    [Route("/users/{userId:long}/[controller]")]
    public class GamesController : ControllerBase
    {
        public GamesController()
        {

        }

        [HttpGet]
        public Task<ActionResult<User>> GetAsync(long userId)
        {
            throw new NotImplementedException();
        }
    }
}
