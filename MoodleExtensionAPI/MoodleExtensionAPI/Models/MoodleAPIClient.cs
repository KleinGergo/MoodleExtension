using Newtonsoft.Json;

namespace MoodleExtensionAPI.Models
{
    public class MoodleAPIClient
    {
        private readonly HttpClient _httpClient;
        private readonly string _baseUri;

        public MoodleAPIClient()
        {
            _httpClient = new HttpClient();
            _baseUri = Constants.MoodleApiUri;
        }
        public async Task<List<APIUsersResponse>> GetAPIUsersForCourse(int courseID)
        {
            List<APIUsersResponse> users = new List<APIUsersResponse>();
            {
                string fullUri = $"{_baseUri}";
                try
                {
                    var formData = new Dictionary<string, string>
                {
                    { "wstoken", Constants.MoodleApiWSToken },
                    { "wsfunction", Constants.MoodleApiGetUsersForCourseEndpoint },
                    { "moodlewsrestformat", Constants.MoodleWSFormat },
                        {"courseid",  courseID.ToString()}
                    // Add other form fields as needed
                };

                    var content = new FormUrlEncodedContent(formData);
                    HttpResponseMessage response = await _httpClient.PostAsync(fullUri, content);

                    if (response.IsSuccessStatusCode)
                    {
                        string responseContent = await response.Content.ReadAsStringAsync();

                        // Deserialize the JSON response into a list of APICourseResponse objects
                        users = JsonConvert.DeserializeObject<List<APIUsersResponse>>(responseContent);
                        return users;
                    }
                    else
                    {
                        throw new Exception($"Error: {response.StatusCode} - {response.ReasonPhrase}");
                    }

                }
                catch (Exception ex)
                {
                    throw ex;

                }
            }

        }
        public async Task<APIGradesResponse> GetTestResultsForCourse(string courseID)
        {
            APIGradesResponse grades = new APIGradesResponse();
            string fullUri = $"{_baseUri}";
            try
            {
                var formData = new Dictionary<string, string>
                {
                    { "wstoken", Constants.MoodleApiWSToken },
                    { "wsfunction", Constants.MoodleApiGetGradeItems },
                    { "moodlewsrestformat", Constants.MoodleWSFormat },
                    {"courseid", courseID }
                };
                var content = new FormUrlEncodedContent(formData);
                HttpResponseMessage response = await _httpClient.PostAsync(fullUri, content);

                if (response.IsSuccessStatusCode)
                {
                    string responseContent = await response.Content.ReadAsStringAsync();

                    // Deserialize the JSON response into a list of APICourseResponse objects
                    grades = JsonConvert.DeserializeObject<APIGradesResponse>(responseContent);
                    return grades;
                }
                else
                {
                    throw new Exception($"Error: {response.StatusCode} - {response.ReasonPhrase}");
                }
            }
            catch (Exception ex)
            {
                throw ex;

            }
        }
        public async Task<List<APICourseResponse>> GetAPICourses()
        {
            List<APICourseResponse> courses = new List<APICourseResponse>();
            string fullUri = $"{_baseUri}";
            try
            {
                var formData = new Dictionary<string, string>
                {
                    { "wstoken", Constants.MoodleApiWSToken },
                    { "wsfunction", Constants.MoodleApiGetCoursesEndpoint },
                    { "moodlewsrestformat", Constants.MoodleWSFormat }
                    // Add other form fields as needed
                };

                var content = new FormUrlEncodedContent(formData);
                HttpResponseMessage response = await _httpClient.PostAsync(fullUri, content);

                if (response.IsSuccessStatusCode)
                {
                    string responseContent = await response.Content.ReadAsStringAsync();

                    // Deserialize the JSON response into a list of APICourseResponse objects
                    courses = JsonConvert.DeserializeObject<List<APICourseResponse>>(responseContent);
                    return courses;
                }
                else
                {
                    throw new Exception($"Error: {response.StatusCode} - {response.ReasonPhrase}");
                }

            }
            catch (Exception ex)
            {
                throw ex;

            }

        }
    }
}
