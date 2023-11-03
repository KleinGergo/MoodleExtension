using Microsoft.AspNetCore.Mvc;
using MoodleExtensionAPI.Models;

namespace MoodleExtensionAPI.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    public class MoodleAPIController : ControllerBase
    {
        [HttpGet("courses")]
        public async Task<IActionResult> GetCourses()
        {
            MoodleAPIClient client = new MoodleAPIClient();
            List<APICourseResponse> courses = await client.GetAPICourses();

            return Ok(courses);
        }

    }
}
