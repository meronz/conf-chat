using Microsoft.AspNetCore.Mvc;

namespace ConfChat.Server.Controllers;

[ApiController]
[Route("[controller]")]
public class HealthController : ControllerBase
{
    /// <summary>
    ///     Query data from collections or views
    /// </summary>
    /// <param name="dataRequest">Data request object</param>
    /// <returns></returns>
    [HttpGet]
    public IActionResult Get()
    {
        return Ok();
    }
}
