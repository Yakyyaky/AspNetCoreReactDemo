using System.Threading.Tasks;
using AspNetCoreReactDemo.Extensions;
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
            if (!model.DenyUnlessLoggedIn 
                || model.DenyUnlessLoggedIn && User.Identity.IsAuthenticated)
            {
                // allow anonymous
                if (await _storage.SaveData(model.SomeOtherField))
                {
                    return Ok(new ModelResult<ModelType1>(true, model));
                }

                return Problem("failed to commit your data", null, StatusCodes.Status500InternalServerError);
            }

            return Unauthorized();
        }

        [HttpPost("model2")]
        public async Task<IActionResult> HandleModel2([FromBody] ModelType2 model)
        {
            if (await _storage.SaveData(model.SomeOtherField))
            {
                return Ok(new ModelResult<ModelType2>(true, model));
            }

            return Problem("failed to commit your data", null, StatusCodes.Status500InternalServerError);
        }       
    }
}