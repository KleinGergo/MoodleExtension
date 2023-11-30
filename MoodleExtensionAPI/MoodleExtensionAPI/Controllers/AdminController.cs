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
                    return Unauthorized("Unauthorized");
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
        [HttpPost("AddPasswordToTeacher")]
        public async Task<IActionResult> AddPaswordToTeacher(string token, string email, string password)
        {
            try
            {
                if (token != Constants.AuthorizationToken)
                {
                    return Unauthorized
                        ("Unauthorized");
                }
                Teacher teacher = DatabaseUtils.getTeacherByEmail(email);
                if (teacher == null)
                {
                    return BadRequest("Thish email does not exists in the database.");
                }
                DatabaseUtils.AddPasswordToTeacher(teacher, password);
                return Ok();

            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }


    }
}
