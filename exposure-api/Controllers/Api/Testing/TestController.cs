using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace exposure_api.Controllers.Api.Testing
{
    [Produces("application/json")]
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class TestController : ControllerBase
    {
        [HttpPost]
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IActionResult endpoint()
        {
            return StatusCode(StatusCodes.Status200OK, new { status = "ok" });
        }
    }
}
