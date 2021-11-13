using HelloLogging.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace HelloLogging.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly IUserService userService;
        private readonly ILogger<UsersController> logger;

        public UsersController(IUserService userService, ILogger<UsersController> logger)
        {
            this.userService = userService;
            this.logger = logger;
        }

        [HttpGet("", Name ="GetUsers")]
        public async Task<IActionResult> GetUsers()
        {
            var users = await userService.GetUsers();
            return Ok(users);
        }

        [HttpGet("{id}", Name = "GetUser")]
        public async Task<IActionResult> GetUser([FromRoute]int id)
        {
            logger.LogInformation("Start calling get user Api..");

            var user = await userService.GetUser(id);

            logger.LogInformation("User: {@User}", user);

            return Ok(user);
        }
    }
}
