using MoodleExtensionAPI.Data;

namespace MoodleExtensionAPI.Utils
{
    public static class DatabaseUtils
    {
        static public bool IsLoginSuccessful(string username, string password)
        {
            using (var context = new Context())
            {
                foreach (var teacher in context.Teachers)
                {
                    if (teacher.Name == username && teacher.PasswordDb == password)
                    {
                        return true;
                    }
                }
            }

            return false;
        }
    }
}
