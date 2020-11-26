using Microsoft.AspNetCore.Mvc;

namespace OnboardingSIGDB1.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PIngController : ControllerBase
    {
        // GET api/values
        [HttpGet]
        public ActionResult<string> Get()
        {
            return Ok("pong");
        }
    }
}
