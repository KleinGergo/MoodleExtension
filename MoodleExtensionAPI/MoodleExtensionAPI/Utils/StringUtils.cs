namespace MoodleExtensionAPI.Utils
{
    public static class StringUtils
    {
        public static string GetNeptuncode(string email)
        {
            string[] parts = email.Split('@');
            return parts[0];
        }
        public static string GetCorrectedTestName(string correctionTestName)
        {
            string[] parts = correctionTestName.Split(' ');
            return parts[0];
        }

    }

}
