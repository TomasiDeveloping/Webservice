using Microsoft.AspNetCore.Mvc;
using Webservice.API.Authentication;

namespace Webservice.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TestController : ControllerBase
    {
        [HttpGet]
        [ApiKeyAuthorize]
        public IActionResult Get()
        {
            return Ok("Hello, World!");
        }

        [HttpGet("jwt")]
        [JwtAuthorize]
        public IActionResult GetJwt()
        {
            return Ok("Hello, JWT World!");

        }

        [HttpGet("smart")]
        [JwtOrApiKeyAuthorize]
        public IActionResult GetSmart()
        {
            return Ok("Hello, Smart World!");
        }
    }
}
