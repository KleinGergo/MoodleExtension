using MoodleExtensionAPI.Models;

namespace MoodleExtensionAPI.Utils
{
    public static class Utils
    {
        public static APICourseResponse GetCourseFromAPIResponse(List<APICourseResponse> list, string course)
        {
            for (int i = 0; i < list.Count; i++)
            {
                if (list[i] != null)
                {
                    if (list[i].displayname.Contains(course))
                    {
                        return list[i];
                    }
                }

            }
            return null;

        }
    }
}

