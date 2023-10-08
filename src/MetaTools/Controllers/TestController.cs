using Microsoft.AspNetCore.Mvc;

namespace MetaTools.Controllers;
[ApiController]
[Route("[controller]")]
public class TestController : ControllerBase
{
    [HttpGet("GET")]
    public IActionResult Get()
    {
        return Ok("OK");
    }
}