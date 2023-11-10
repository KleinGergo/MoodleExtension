using Microsoft.EntityFrameworkCore;
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

                foreach (var course in courses)
                {

                    var existingSubject = context.Subjects.FirstOrDefault(s => s.SubjectMoodleID == course.id);

                    if (existingSubject == null)
                    {
                        // If it doesn't exist, add the subject
                        Subject sub = new Subject();
                        sub.SubjectMoodleID = course.id;
                        sub.SubjectName = course.displayname;
                        sub.SignatureCondition = "N/A";
                        context.Subjects.Add(sub);
                    }
                }
                context.SaveChanges();
            }
        }

        public static Student GetStudentByNeptuncode(string Neptuncode)
        {
            using (var context = new Context())
            {
                return context.Students.FirstOrDefault(student => student.Neptuncode == Neptuncode);
            }
        }
        public static Student GetStudentByMoodleID(int moodleID)
        {
            using (var context = new Context())
            {
                return context.Students.FirstOrDefault(student => student.MoodleID == moodleID);
            }
        }
        public static void SaveUsers(List<APIUsersResponse> users)
        {
            using (var context = new Context())
            {

                foreach (var user in users)
                {
                    if (user.roles[0].roleid == Constants.StudentRoleID)
                    {
                        var existingStudent = context.Students.FirstOrDefault(s => s.MoodleID == user.id);

                        if (existingStudent == null)
                        {

                            Student stud = new Student();
                            stud.Neptuncode = StringUtils.GetNeptuncode(user.email);
                            stud.MoodleID = user.id;
                            stud.Email = user.email;
                            stud.FirstName = user.firstname; stud.LastName = user.lastname;
                            context.Students.Add(stud);

                        }
                    }
                    else if (user.roles[0].roleid == Constants.TeacherRoleID)
                    {
                        var existingTeacher = context.Teachers.FirstOrDefault(t => t.MoodleID == user.id);
                        if (existingTeacher == null)
                        {
                            Teacher teacher = new Teacher();
                            teacher.MoodleID = user.id;
                            teacher.Email = user.email;
                            teacher.Name = user.fullname;
                            teacher.IsPasswordChanged = false;
                            context.Teachers.Add(teacher);
                        }
                    }


                }
                context.SaveChanges();
            }
        }
        public static void SaveTests(Subject sub, APIGradesResponse tests)
        {
            using (var context = new Context())
            {
                foreach (var grades in tests.usergrades)
                {

                    foreach (var item in grades.gradeitems)
                    {
                        if (!IsTestSaved(item.id, grades.userid) && item.itemmodule == Constants.Quiz)
                        {

                            Test newTest = new Test
                            {
                                MoodleTestID = item.id,
                                GradeMax = item.grademax,
                                GradeMin = item.grademin,
                                Result = item.graderaw,
                                Type = item.itemname,
                                IsCompleted = (item.gradedatesubmitted != null),
                                Subject = context.Subjects.FirstOrDefault(s => s.SubjectMoodleID == sub.SubjectMoodleID),
                                Student = context.Students.FirstOrDefault(s => s.MoodleID == grades.userid)
                            };
                            context.Tests.Add(newTest);

                        }

                    }
                }
                // Save changes to the database, including the tests and their associations with students.
                context.SaveChanges();
            }
        }
        public static List<Test> GetStudentTestResults(string Neptuncode, Subject sub)
        {
            using (var context = new Context())
            {
                List<Test> tests = context.Tests
                    .Where(e => e.Student.Neptuncode == Neptuncode && e.Subject.SubjectID == sub.SubjectID)
                    .ToList();

                return tests;
            }
        }
        public static List<Test> GetTestsForSubject(Subject sub)
        {
            using (var context = new Context())
            {

                List<Test> tests = context.Tests.Include(e => e.Student).Where(e => e.Subject.SubjectID == sub.SubjectID).Select(p => new
                {
                    p.TestID,
                    p.Result,
                    p.Student,
                    p.GradeMax,
                    p.MoodleTestID,
                    p.IsCompleted,
                    p.Type,
                    p.PreviousTestID,
                    p.Label
                }).AsEnumerable().Select(
                    p => new Test
                    {
                        Result = p.Result,
                        TestID = p.TestID,
                        GradeMax = p.GradeMax,
                        MoodleTestID = p.MoodleTestID,
                        Student = p.Student,
                        IsCompleted = p.IsCompleted,
                        Type = p.Type,
                        PreviousTestID = p.PreviousTestID,
                        Label = p.Label
                    }).ToList();

                return tests;
            }

        }


        public static bool IsTestSaved(int moodleTestID, int studentMoodleID)
        {
            using (var context = new Context())
            {
                return (context.Tests.FirstOrDefault(t => t.MoodleTestID == moodleTestID && t.Student.MoodleID == studentMoodleID) != null);
            }

        }
        public static bool IsTestWritten(int moodleTestID, int studentMoodleID)
        {
            using (var context = new Context())
            {
                if (context.Tests.FirstOrDefault(t => t.MoodleTestID == moodleTestID && t.Student.MoodleID == studentMoodleID) != null)
                {
                    return (context.Tests.FirstOrDefault(t => t.MoodleTestID == moodleTestID && t.Student.MoodleID == studentMoodleID).Result != null);
                }
                return false;
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
