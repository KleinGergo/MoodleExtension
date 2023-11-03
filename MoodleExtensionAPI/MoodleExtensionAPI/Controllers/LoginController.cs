using Microsoft.AspNetCore.Mvc;
using MoodleExtensionAPI.Utils;

namespace MoodleExtensionAPI.Controllers
{
    public class LoginResponse
    {
        public bool isLoginSuccessful { get; set; }
        public bool isPasswordChanged { get; set; }
        public LoginResponse()
        {
        }
    }

    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        [HttpPost("login")]
        public JsonResult Login(string email, string password)
        {
            LoginResponse resp = new LoginResponse();
            if (DatabaseUtils.IsLoginSuccessful(email, password))
            {

                resp.isLoginSuccessful = true;
                resp.isPasswordChanged = DatabaseUtils.IsPasswordChanged(email, password);
                return new JsonResult(resp);
            }
            resp.isLoginSuccessful = false;
            resp.isPasswordChanged = false;

            return new JsonResult(resp);

        }

    }
}
