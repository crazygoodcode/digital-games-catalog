using GamesCatalog.Core.Data;
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

        public UsersController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        [HttpGet("{id:long}", Name = "Get")]
        public async Task<ActionResult<User>> GetAsync(long id, CancellationToken cancellationToken = default)
        {
            try
            {
                var user = await _userRepository.GetAsync(id, cancellationToken);

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
                    var id = await _userRepository.AddAsync(user, cancellationToken);

                    return CreatedAtAction("Get", new { id = id }, user);
                }
                catch (Exception ex)
                {
                    // Log - prod system would not return ex.Message
                    return StatusCode(500, ex.Message);
                }
            }

            return BadRequest();
        }
    }
}
