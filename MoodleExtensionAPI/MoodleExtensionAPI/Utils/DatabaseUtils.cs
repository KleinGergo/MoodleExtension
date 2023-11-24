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
        public static async void SaveUsersForAllCourse(MoodleAPIClient client)
        {
            List<Subject> subjects = new List<Subject>();
            using (var context = new Context())
            {
                subjects = context.Subjects.ToList();
            }
            foreach (var subject in subjects)
            {
                if (subject != null)
                {
                    List<APIUsersResponse> users = await client.GetAPIUsersForCourse(subject.SubjectMoodleID);
                    if (users != null)
                        DatabaseUtils.SaveUsers(users, subject.SubjectID);
                }
            }
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




        public static Subject GetSubjectByTests(List<Test> test)
        {
            if (test.Count > 0)
            {
                using (var context = new Context())
                {
                    return context.Subjects.FirstOrDefault(s => s.SubjectID == test[0].Subject.SubjectID);
                }
            }
            return null;

        }

        public static void SaveUsers(List<APIUsersResponse> users, int subjectID)
        {
            using (var context = new Context())
            {
                foreach (var user in users)
                {
                    if (user.roles.Count > 0)
                    {
                        if (user.roles[0].roleid == Constants.StudentRoleID)
                        {
                            Student existingStudent = context.Students.FirstOrDefault(s => s.MoodleID == user.id);

                            if (existingStudent == null)
                            {

                                Student stud = new Student();
                                stud.Neptuncode = StringUtils.GetNeptuncode(user.email);
                                stud.MoodleID = user.id;
                                stud.Email = user.email;
                                stud.FirstName = user.firstname; stud.LastName = user.lastname;
                                context.Students.Add(stud);
                                Subject sub = context.Subjects.FirstOrDefault(t => t.SubjectID == subjectID);
                                TakenCourse existingTakenCourse = context.TakenCourses.FirstOrDefault(t => t.Subject.SubjectID == subjectID && t.Student.MoodleID == user.id);

                                if (existingTakenCourse == null)
                                {
                                    TakenCourse takenCourse = new TakenCourse(sub);
                                    takenCourse.Student = stud;
                                    context.TakenCourses.Add(takenCourse);
                                }
                                else
                                {
                                    existingTakenCourse.Student = context.Students.FirstOrDefault(s => s.ID == existingStudent.ID);
                                }
                            }
                            else
                            {
                                Subject sub = context.Subjects.FirstOrDefault(t => t.SubjectID == subjectID);
                                TakenCourse existingTakenCourse = context.TakenCourses.FirstOrDefault(t => t.Subject.SubjectID == subjectID && t.Student.MoodleID == user.id);
                                if (existingTakenCourse == null)
                                {
                                    TakenCourse takenCourse = new TakenCourse(sub);
                                    takenCourse.Student = context.Students.FirstOrDefault(s => s.MoodleID == user.id);
                                    context.TakenCourses.Add(takenCourse);
                                }
                                else
                                {
                                    existingTakenCourse.Student = context.Students.FirstOrDefault(s => s.ID == existingStudent.ID);
                                }
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
                                Label = item.itemname,
                                IsCompleted = (item.gradedatesubmitted != null),
                                Subject = context.Subjects.FirstOrDefault(s => s.SubjectMoodleID == sub.SubjectMoodleID),
                                Student = context.Students.FirstOrDefault(s => s.MoodleID == grades.userid),

                            };
                            if (item.itemname.Contains("NagyZH"))
                            {
                                newTest.Type = Constants.TypeBigTests;
                            }
                            else if (item.itemname.Contains("KisZH"))
                            {
                                newTest.Type = Constants.TypeSmallTests;
                            }
                            else if (item.itemname.Contains("Beadandó"))
                            {
                                newTest.Type = Constants.TypeMultipleAssigment;
                            }
                            context.Tests.Add(newTest);

                        }
                        else if (item.itemmodule == Constants.Quiz)
                        {
                            Test savedTest = context.Tests.FirstOrDefault(t => t.MoodleTestID == item.id && t.Student.MoodleID == grades.userid);
                            savedTest.Result = item.graderaw;
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
                    p.Result,
                    p.Student,
                    p.GradeMax,
                    p.Type,
                }).AsEnumerable().Select(
                    p => new Test
                    {
                        Result = p.Result,
                        GradeMax = p.GradeMax,
                        Student = p.Student,
                        Type = p.Type,
                    }).ToList();

                return tests;
            }

        }
        public static FrontendStatistics GetStatisticsForSubject(List<StudentTestSignatureDTO> studentTestSignatureDTOs, string subjectName)
        {
            FrontendStatistics frontendStatistics = new FrontendStatistics();
            frontendStatistics.SubjectName = subjectName;
            frontendStatistics.TotalSignatures = 0;
            frontendStatistics.TotalNumberOfPassedStudents = 0;
            frontendStatistics.TotalTests = 0;
            FrontendChartData frontendChartDataGradeA = new FrontendChartData(Constants.GradeA);
            FrontendChartData frontendChartDataGradeB = new FrontendChartData(Constants.GradeB);
            FrontendChartData frontendChartDataGradeC = new FrontendChartData(Constants.GradeC);
            FrontendChartData frontendChartDataGradeD = new FrontendChartData(Constants.GradeD);
            FrontendChartData frontendChartDataGradeF = new FrontendChartData(Constants.GradeF);
            foreach (var st in studentTestSignatureDTOs)
            {
                frontendStatistics.TotalTests += st.Tests.Count;
                if (st.SignatureStatus == Constants.SignatureApproved)
                    frontendStatistics.TotalSignatures += 1;
                if (st.Grade != Constants.GradeF)
                    frontendStatistics.TotalNumberOfPassedStudents += 1;
                if (st.Grade == Constants.GradeF)
                    frontendChartDataGradeF.Jegy++;
                if (st.Grade == Constants.GradeD)
                    frontendChartDataGradeD.Jegy++;
                if (st.Grade == Constants.GradeC)
                    frontendChartDataGradeC.Jegy++;
                if (st.Grade == Constants.GradeB)
                    frontendChartDataGradeB.Jegy++;
                if (st.Grade == Constants.GradeA)
                    frontendChartDataGradeA.Jegy++;
            }
            frontendStatistics.frontendChartData.Add(frontendChartDataGradeA);
            frontendStatistics.frontendChartData.Add(frontendChartDataGradeB);
            frontendStatistics.frontendChartData.Add(frontendChartDataGradeC);
            frontendStatistics.frontendChartData.Add(frontendChartDataGradeD);
            frontendStatistics.frontendChartData.Add(frontendChartDataGradeF);

            return frontendStatistics;

        }
        public static List<StudentTestSignatureDTO> GetStudentTestSignatures(Subject sub)
        {
            List<StudentTestSignatureDTO> results = new List<StudentTestSignatureDTO>();
            List<Student> students = new List<Student>();
            using (var context = new Context())
            {
                students = context.TakenCourses
              .Include(tc => tc.Student)
              .Where(tc => tc.Subject.SubjectID == sub.SubjectID)
              .Select(tc => tc.Student)
              .ToList();

                foreach (var stud in students)
                {
                    List<Test> studTests = GetTestsForStudent(stud, sub);
                    SignatureCondition signatureCondition = sub.GetSignatureCondition();
                    bool isSignatureApproved = sub.IsSignatureApproved(studTests, signatureCondition);
                    StudentTestSignature signature = new StudentTestSignature();
                    if (isSignatureApproved)
                    {
                        signature.Student = stud;
                        signature.tests = studTests;
                        signature.SignatureStatus = "approved";
                    }
                    else
                    {
                        signature.Student = stud;
                        signature.tests = studTests;
                        signature.SignatureStatus = "denied";
                    }
                    signature.Grade = Utils.CalculateFinalGrade(signature, sub.GetSignatureCondition());
                    OfferedGradeCondition condition = sub.GetOfferedGradeCondition();
                    bool isOfferedGradeApproved = sub.IsOfferedGradeApproved(studTests, condition);
                    signature.OfferedGrade = Utils.CalculateOfferedGrade(signature, condition, isOfferedGradeApproved);
                    var signatureDTO = new StudentTestSignatureDTO
                    {
                        NeptunCode = stud.Neptuncode,
                        Tests = studTests.Select(test => new TestResultDTO
                        {
                            TestName = test.Label,
                            Result = test.Result,
                            Type = test.Type,
                            GradeMax = test.GradeMax// Assuming Test class has a property named "Result"
                        }).ToList(),
                        SignatureStatus = signature.SignatureStatus,
                        Grade = signature.Grade,
                        OfferedGrade = signature.OfferedGrade
                    };
                    TakenCourse takenCourse = context.TakenCourses.FirstOrDefault(tc => tc.Subject.SubjectID == sub.SubjectID && tc.Student.Neptuncode == stud.Neptuncode);
                    takenCourse.SignatureState = signature.SignatureStatus;
                    takenCourse.Grade = signature.Grade;
                    takenCourse.OfferedGrade = signature.OfferedGrade;
                    context.SaveChanges();
                    results.Add(signatureDTO);

                }
            }

            return results;

        }
        public static List<Test> GetTestsForStudent(Student student, Subject sub)
        {
            List<Test> tests = new List<Test>();
            using (var context = new Context())
            {
                tests = context.Subjects
            .Include(s => s.Tests)
            .Where(s => s.SubjectID == sub.SubjectID) // Assuming SubjectID is the identifier for the subject
            .SelectMany(s => s.Tests)
            .Where(t => t.Student.ID == student.ID)
            .ToList();
            }
            return tests;
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
        public static void AddOfferedGradeConditionToSubject(Subject sub, string offeredGradeCondition)
        {
            using (var context = new Context())
            {
                foreach (var subject in context.Subjects)
                {
                    if (subject.SubjectID == sub.SubjectID)
                    {
                        subject.OfferedGradeCondition = offeredGradeCondition;
                    }
                }
                context.SaveChanges();
            }
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
