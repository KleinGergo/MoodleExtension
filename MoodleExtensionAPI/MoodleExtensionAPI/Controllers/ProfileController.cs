using Microsoft.AspNetCore.Mvc;
using MoodleExtensionAPI.Utils;

namespace MoodleExtensionAPI.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    public class ProfileController : ControllerBase
    {
        [HttpPost("updatePassword")]
        public IActionResult UpdatePassword(string email, string currentPassword, string newPassword)
        {

            if (DatabaseUtils.IsLoginSuccessful(email, currentPassword))
            {
                DatabaseUtils.UpdatePassword(email, currentPassword, newPassword);
                return Ok();
            }
            else
            {
                return Unauthorized("Login credentials are invalid");
            }

        }


    }
}
