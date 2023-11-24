using Microsoft.AspNetCore.Mvc;
using MoodleExtensionAPI.Models;
using MoodleExtensionAPI.Utils;

namespace MoodleExtensionAPI.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    public class AdminController : ControllerBase
    {
        MoodleAPIClient client = new MoodleAPIClient();
        [HttpPost("Initialization")]
        public async Task<IActionResult> Initialization(string token)
        {
            try
            {
                if (token != Constants.AuthorizationToken)
                {
                    return BadRequest("Unauthorized");
                }
                List<APICourseResponse> courses = await client.GetAPICourses();
                if (courses != null)
                {
                    DatabaseUtils.SaveCourses(courses);
                    DatabaseUtils.SaveUsersForAllCourse(client);
                }

                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }



        }


    }
}
