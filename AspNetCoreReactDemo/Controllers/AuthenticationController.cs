using System.Threading.Tasks;
using AspNetCoreReactDemo.Model;
using AspNetCoreReactDemo.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AspNetCoreReactDemo.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly IAuthenticationManager _authenticationManager;

        public AuthenticationController(IAuthenticationManager authenticationManager)
        {
            _authenticationManager = authenticationManager;
        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> SignIn([FromBody] SignInCredential signInCredential)
        {
            var user = await _authenticationManager.SignIn(signInCredential);
            if (user == null) return Unauthorized();
            return Ok(user);
        }

        [HttpPost("SignOut")]
        public async Task<IActionResult> SignOut([FromBody] SignOutUser user)
        {
            var result = await _authenticationManager.SignOut(user.Upn);
            return Ok(result);
        }
    }
}