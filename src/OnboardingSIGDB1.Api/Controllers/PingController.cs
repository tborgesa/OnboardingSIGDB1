using Microsoft.AspNetCore.Mvc;
using OnboardingSIGDB1.Api._Base.Controllers;

namespace OnboardingSIGDB1.Api.Controllers
{
    public class PingController : OnboardingSIGDB1Controller
    {
        [HttpGet]
        public ActionResult<string> Get()
        {
            return Ok("pong");
        }
    }
}
