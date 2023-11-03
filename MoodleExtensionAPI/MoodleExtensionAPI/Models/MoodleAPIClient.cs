using Newtonsoft.Json;
using System.Text;

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

        public async Task<List<APICourseResponse>> GetAPICourses()
        {
            List<APICourseResponse> courses = new List<APICourseResponse>();
            string fullUri = $"{_baseUri}";
            try
            {
                Dictionary<string, string> data = new Dictionary<string, string>();
                data.Add("wstoken", Constants.MoodleApiWSToken);
                data.Add("wsfunction", Constants.MoodleApiGetCoursesEndpoint);
                data.Add("moodlewsformat", Constants.MoodleWSFormat);
                string jsonData = JsonConvert.SerializeObject(data);
                var content = new StringContent(jsonData, Encoding.UTF8, "application/json");
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
