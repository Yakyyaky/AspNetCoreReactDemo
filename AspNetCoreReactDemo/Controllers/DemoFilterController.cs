using System.Threading.Tasks;
using AspNetCoreReactDemo.Extensions;
using AspNetCoreReactDemo.Model;
using AspNetCoreReactDemo.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AspNetCoreReactDemo.Controllers
{
    [Authorize("DenyUnlessLoggedIn")]
    [ApiController]
    [Route("api/[controller]")]
    public class DemoFilterController : ControllerBase
    {
        private readonly IDemoModelStorage _storage;

        public DemoFilterController(IDemoModelStorage storage)
        {
            _storage = storage;
        }

        [HttpPost]
        public async Task<ActionResult<ModelResult<ModelType>>> HandleModel([FromBody] ModelType model)
        {
            if (await _storage.SaveData(model.SomeOtherField))
            {
                return new ModelResult<ModelType>(true, model);
            }

            return Problem("failed to commit your data", null, StatusCodes.Status500InternalServerError);
        }
    }
}