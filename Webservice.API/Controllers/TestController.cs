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
            return Ok("Hello, API Key!");
        }

        [HttpGet("jwt")]
        [JwtAuthorize]
        public IActionResult GetJwt()
        {
            return Ok("Hello, JWT!");

        }

        [HttpGet("smart")]
        [JwtOrApiKeyAuthorize]
        public IActionResult GetSmart()
        {
            return Ok("Hello, Smart!");
        }

        [HttpGet("public")]
        public IActionResult GetPublic([FromQuery] int value)
        {
            var result = 10 / value; // This will throw if value is 0, testing global exception handling
            return Ok(result);
        }
    }
}
