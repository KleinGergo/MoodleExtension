using Microsoft.AspNetCore.Mvc;
using MoodleExtensionAPI.Models;
using MoodleExtensionAPI.Utils;

namespace MoodleExtensionAPI.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    public class MoodleAPIController : ControllerBase
    {
        MoodleAPIClient client = new MoodleAPIClient();
        [HttpGet("courses")]
        public async Task<IActionResult> GetCourses()
        {

            List<APICourseResponse> courses = await client.GetAPICourses();
            DatabaseUtils.SaveCourses(courses);

            return Ok(courses);
        }
        [HttpGet("results")]
        public async Task<IActionResult> GetResults(string courseIdentifier)
        {

            APIGradesResponse grades = await client.GetTestResultsForCourse(courseIdentifier);
            return Ok(grades);

        }
        [HttpPut("addSignatureCondition")]
        public async Task<IActionResult> AddSignatureCondition(string subjectIdentifier, string signatureCondition)
        {
            if (DatabaseUtils.CheckIfSubjectExists(subjectIdentifier))
            {
                Subject subject = DatabaseUtils.GetSubject(subjectIdentifier);
                DatabaseUtils.AddSignatureConditionToSubject(subject, signatureCondition);
            }
            else
            {
                List<APICourseResponse> courses = await client.GetAPICourses();
                DatabaseUtils.SaveCourses(courses);
                if (!DatabaseUtils.CheckIfSubjectExists(subjectIdentifier))
                {
                    return BadRequest("The subject does not exists!");
                }
                else
                {
                    Subject subject = DatabaseUtils.GetSubject(subjectIdentifier);
                    DatabaseUtils.AddSignatureConditionToSubject(subject, signatureCondition);
                }
            }
            return Ok();
        }

    }
}
