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

        [HttpGet("saveUsers")]
        public async Task<IActionResult> GetStudentsForCourse(string courseIdentifier)
        {
            Subject subject = DatabaseUtils.GetSubject(courseIdentifier);
            if (subject != null)
            {
                List<APIUsersResponse> users = await client.GetAPIUsersForCourse(subject.SubjectMoodleID);
                DatabaseUtils.SaveUsers(users);
            }
            return Ok();
        }
        [HttpGet("saveCourses")]
        public async Task<IActionResult> GetCourses()
        {

            List<APICourseResponse> courses = await client.GetAPICourses();
            DatabaseUtils.SaveCourses(courses);

            return Ok(courses);
        }
        [HttpGet("getStudentResults")]
        public async Task<IActionResult> GetResults(string Neptuncode, string courseIdentifier)
        {
            Subject sub = DatabaseUtils.GetSubject(courseIdentifier);
            if (sub != null)
            {
                List<Test> tests = DatabaseUtils.GetStudentTestResults(Neptuncode, sub);
                if (tests != null)
                    return Ok(tests);
            }

            return BadRequest("No test results for this student in this course!");
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
        [HttpGet("getTestResults")]
        public async Task<JsonResult> GetTestResultsForCourse(string subjectIdentifier)
        {
            if (!DatabaseUtils.CheckIfSubjectExists(subjectIdentifier))
            {
                return null;
            }

            Subject sub = DatabaseUtils.GetSubject(subjectIdentifier);
            APIGradesResponse grades = await client.GetTestResultsForCourse(sub.SubjectMoodleID.ToString());
            DatabaseUtils.SaveTests(sub, grades);
            List<Test> tests = DatabaseUtils.GetTestsForSubject(sub);

            return new JsonResult(tests);
        }

    }
}
