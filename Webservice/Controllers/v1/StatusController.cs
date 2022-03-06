using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Webservice.Controllers.v1;

[ApiVersion("1.0")]
[Route("api/v{v:apiVersion}/[controller]")]
[ApiController]
[Authorize(Roles = "Admin")]
public class StatusController : ControllerBase
{
    [HttpGet]
    public ActionResult CheckStatus()
    {
        return Ok("Service is running...");
    }
}