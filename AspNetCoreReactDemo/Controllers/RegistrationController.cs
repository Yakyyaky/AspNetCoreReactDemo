using System.Net;
using System.Threading.Tasks;
using AspNetCoreReactDemo.Model;
using AspNetCoreReactDemo.Services;
using Microsoft.AspNetCore.Mvc;

namespace AspNetCoreReactDemo.Controllers
{
    [Route("api/registration")]
    [ApiController]
    public class RegistrationController : ControllerBase
    {
        private readonly IUserService _userService;

        public RegistrationController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost]
        public async Task<ActionResult<User>> Register([FromBody] UserRegistration registration)
        {
            var newUser = await _userService.CreateUser(registration.User, registration.Password);
            if (newUser == null) return BadRequest();
            return newUser;
        }
    }
}