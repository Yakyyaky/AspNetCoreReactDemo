﻿using System.Collections.Generic;
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
        public async Task<IActionResult> SignIn([FromBody] UserCredential userCredential)
        {
            var user = await _authenticationManager.SignIn(userCredential);
            if (user == null) return Unauthorized();
            return Ok(user);
        }

        [HttpPost("LogOut")]
        public async Task<IActionResult> LogOut([FromBody] SignOutUser user)
        {
            var result = await _authenticationManager.LogOut(user.Upn);
            return Ok(result);
        }
    }
}