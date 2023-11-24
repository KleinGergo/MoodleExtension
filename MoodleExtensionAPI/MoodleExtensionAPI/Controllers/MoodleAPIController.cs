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
        public async Task<IActionResult> GetUsersForCourse(string courseIdentifier)
        {
            Subject subject = DatabaseUtils.GetSubject(courseIdentifier);
            if (subject != null)
            {
                List<APIUsersResponse> users = await client.GetAPIUsersForCourse(subject.SubjectMoodleID);
                if (users != null)
                    DatabaseUtils.SaveUsers(users, subject.SubjectID);
            }
            return Ok();
        }
        [HttpGet("saveCourses")]
        public async Task<IActionResult> GetCourses()
        {

            List<APICourseResponse> courses = await client.GetAPICourses();
            if (courses != null)
            {
                DatabaseUtils.SaveCourses(courses);

                return Ok(courses);
            }
            return BadRequest("There are no courses.");

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
            else
            {
                List<APICourseResponse> courses = await client.GetAPICourses();
                if (courses != null)
                {
                    DatabaseUtils.SaveCourses(courses);
                    if (DatabaseUtils.CheckIfSubjectExists(courseIdentifier))
                    {
                        List<Test> tests = DatabaseUtils.GetStudentTestResults(Neptuncode, sub);
                        if (tests != null)
                            return Ok(tests);
                    }
                }


            }

            return BadRequest("No test results for this student in this course!");
        }

        [HttpPut("addOfferedGradeCondition")]
        public async Task<IActionResult> AddOfferedGradeCondition(string subjectIdentifier, string signatureCondition)
        {
            try
            {
                if (DatabaseUtils.CheckIfSubjectExists(subjectIdentifier))
                {
                    Subject subject = DatabaseUtils.GetSubject(subjectIdentifier);
                    DatabaseUtils.AddOfferedGradeConditionToSubject(subject, signatureCondition);

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
                        DatabaseUtils.AddOfferedGradeConditionToSubject(subject, signatureCondition);
                    }

                }
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }
        [HttpPut("addSignatureCondition")]
        public async Task<IActionResult> AddSignatureCondition(string subjectIdentifier, string signatureCondition)
        {
            try
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
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }
        [HttpGet("getTestResults")]
        public async Task<JsonResult> GetTestResultsForCourse(string subjectIdentifier)
        {
            try
            {
                if (!DatabaseUtils.CheckIfSubjectExists(subjectIdentifier))
                {
                    return new JsonResult(BadRequest("The subject does not exists!"));
                }

                Subject sub = DatabaseUtils.GetSubject(subjectIdentifier);
                List<APIUsersResponse> users = await client.GetAPIUsersForCourse(sub.SubjectMoodleID);
                DatabaseUtils.SaveUsers(users, sub.SubjectID);
                APIGradesResponse grades = await client.GetTestResultsForCourse(sub.SubjectMoodleID.ToString());
                DatabaseUtils.SaveTests(sub, grades);
                List<StudentTestSignatureDTO> testResults = DatabaseUtils.GetStudentTestSignatures(sub);
                return new JsonResult(testResults);

            }
            catch (Exception ex)
            {
                return new JsonResult(ex);
            }



        }

        [HttpGet("getStatistics")]
        public async Task<JsonResult> GetStatisticsForCourse(string subjectIdentifier)
        {
            try
            {
                if (!DatabaseUtils.CheckIfSubjectExists(subjectIdentifier))
                {
                    await GetCourses();
                    if (!DatabaseUtils.CheckIfSubjectExists(subjectIdentifier))
                    {
                        return new JsonResult(BadRequest("The subject does not exists!"));
                    }

                }
                Subject sub = DatabaseUtils.GetSubject(subjectIdentifier);
                APIGradesResponse grades = await client.GetTestResultsForCourse(sub.SubjectMoodleID.ToString());
                DatabaseUtils.SaveTests(sub, grades);
                List<Test> tests = DatabaseUtils.GetTestsForSubject(sub);
                List<StudentTestSignatureDTO> testResults = DatabaseUtils.GetStudentTestSignatures(sub);
                FrontendStatistics frontedData = DatabaseUtils.GetStatisticsForSubject(testResults, sub.SubjectName);
                return new JsonResult(frontedData);
            }
            catch (Exception ex)
            {
                return new JsonResult(ex);
            }

        }

    }
}
