using MoodleExtensionAPI.Data;
using MoodleExtensionAPI.Models;

namespace MoodleExtensionAPI.Utils
{
    public static class DatabaseUtils
    {
        public static bool IsLoginSuccessful(string email, string password)
        {
            using (var context = new Context())
            {
                foreach (var teacher in context.Teachers)
                {
                    if (teacher.Email == email && teacher.PasswordDb == password)
                    {
                        return true;
                    }
                }
            }

            return false;
        }
        public static void UpdatePassword(string email, string currentPassword, string newPassword)
        {
            using (var context = new Context())
            {
                foreach (var teacher in context.Teachers)
                {
                    if (teacher.Email == email && teacher.PasswordDb == currentPassword)
                    {
                        teacher.PasswordDb = newPassword;
                        teacher.IsPasswordChanged = true;
                    }
                }
                context.SaveChanges();
            }
        }
        public static bool IsPasswordChanged(string email, string password)
        {
            using (var context = new Context())
            {
                foreach (var teacher in context.Teachers)
                {
                    if (teacher.Email == email && teacher.PasswordDb == password)
                    {
                        return teacher.IsPasswordChanged;
                    }
                }
            }
            return false;
        }

        public static void SaveCourses(List<APICourseResponse> courses)
        {
            using (var context = new Context())
            {
                Department dep = new Department();
                foreach (var course in courses)
                {

                    var existingSubject = context.Subjects.FirstOrDefault(s => s.SubjectMoodleID == course.id);

                    if (existingSubject == null)
                    {
                        // If it doesn't exist, add the subject
                        Subject sub = new Subject();
                        sub.SubjectMoodleID = course.id;
                        sub.SubjectName = course.displayname;
                        sub.Department = dep;
                        sub.SignatureCondition = "N/A";
                        sub.Tests = new List<Test>();
                        context.Subjects.Add(sub);
                    }
                }
                context.SaveChanges();
            }
        }
        public static void SaveStudents(List<APIUsersResponse> students)
        {
            using (var context = new Context())
            {

                foreach (var student in students)
                {
                    Student stud = new Student();
                    stud.Neptuncode = StringUtils.GetNeptuncode(student.email);
                    stud.MoodleID = student.id;
                    stud.Email = student.email;
                    stud.FirstName = student.firstname; stud.LastName = student.lastname;
                    context.Students.Add(stud);
                }
                context.SaveChanges();
            }
        }
        public static Subject GetSubject(string subjectIdentifier)
        {
            using (var context = new Context())
            {
                foreach (var subject in context.Subjects)
                {
                    if (subject.SubjectName.Contains(subjectIdentifier))
                    {
                        return subject;
                    }
                }
            }
            return null;
        }
        public static bool CheckIfSubjectExists(string subjectIdentifier)
        {
            if (string.IsNullOrEmpty(subjectIdentifier)) return false;
            using (var context = new Context())
            {
                foreach (var subject in context.Subjects)
                {
                    if (subject.SubjectName != null)
                    {
                        if (subject.SubjectName.Contains(subjectIdentifier)) return true;
                    }

                }
            }

            return false;
        }
        public static void AddSignatureConditionToSubject(Subject sub, string signatureCondition)
        {
            using (var context = new Context())
            {
                foreach (var subject in context.Subjects)
                {
                    if (subject.SubjectID == sub.SubjectID)
                    {
                        subject.SignatureCondition = signatureCondition;
                    }
                }
                context.SaveChanges();
            }
        }
        public static List<Test> GetResultsForSubject(string subjectIdentifier)
        {
            List<Test> results = new List<Test>();
            using (var context = new Context())
            {
                foreach (var subject in context.Subjects)
                {
                    if (subject.SubjectName.Contains(subjectIdentifier))
                    {
                        results = subject.Tests.ToList();
                    }
                }
            }
            return results;
        }

    }


}
