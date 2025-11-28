using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace _360AI_API.Constrollers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InitDbController : ControllerBase
    {
        [HttpGet("init-db")]
        public IActionResult DatabaseInit()
        {
            var config = "";
            return Ok();
        }
    }
}
