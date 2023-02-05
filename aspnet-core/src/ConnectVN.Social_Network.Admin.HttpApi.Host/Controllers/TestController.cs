using Microsoft.AspNetCore.Mvc;

namespace ConnectVN.Social_Network.Admin.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TestController : ControllerBase
    {
        public TestController()
        {

        }
        [HttpGet]
        public IActionResult TestPhile(int id)
        {
            return Ok(new { id });
        }
    }
}
