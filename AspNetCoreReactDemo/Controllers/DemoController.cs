using System;
using System.Diagnostics;
using System.Threading.Tasks;
using AspNetCoreReactDemo.Model;
using AspNetCoreReactDemo.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;

namespace AspNetCoreReactDemo.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class DemoController : ControllerBase
    {
        private readonly IDemoModelStorage _storage;

        public DemoController(IDemoModelStorage storage)
        {
            _storage = storage;
        }

        [HttpGet]
        public IActionResult Get()
        {
            return Ok("Wee!! you're authenticated to make request");
        }

        [AllowAnonymous]
        [HttpPost("model1")]
        public async Task<IActionResult> HandleModel1([FromBody] ModelType1 model)
        {
            if (!model.DenyUnlessLoggedIn)
            {
                // allow anonymous
                if (await SaveData(model.SomeOtherField))
                {
                    return Ok();
                }

                return Problem("failed to commit your data", null, StatusCodes.Status500InternalServerError);
            }

            return Unauthorized();
        }

        [HttpPost("model2")]
        public async Task<IActionResult> HandleModel2([FromBody] ModelType2 model)
        {
            if (await SaveData(model.SomeOtherField))
            {
                return Ok();
            }

            return Problem("failed to commit your data", null, StatusCodes.Status500InternalServerError);
        }

        private async Task<bool> SaveData(string data)
        {
            var result = await _storage.Update(data);
            if (!result) return false;

            return await _storage.SaveChanges();
        }
    }
}