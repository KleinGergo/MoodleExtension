using Microsoft.AspNetCore.Mvc;
using MoodleExtensionAPI.Utils;

namespace MoodleExtensionAPI.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        [HttpPost("login")]
        public IActionResult Login(string username, string password)
        {

            if (DatabaseUtils.IsLoginSuccessful(username, password))
            {
                return Ok();
            }

            return BadRequest();

        }

    }
}
