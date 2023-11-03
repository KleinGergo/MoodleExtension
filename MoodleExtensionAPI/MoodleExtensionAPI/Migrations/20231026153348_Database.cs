using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MoodleExtensionAPI.Migrations
{
    /// <inheritdoc />
    public partial class Database : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Departments",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Departments", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Semesters",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Year = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Semesters", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Students",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Neptuncode = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MoodleID = table.Column<int>(type: "int", nullable: false),
                    Training = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Students", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Subjects",
                columns: table => new
                {
                    SubjectID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SubjectMoodleID = table.Column<int>(type: "int", nullable: false),
                    SubjectName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    numberOfTests = table.Column<int>(type: "int", nullable: true),
                    SignatureState = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SignatureCondition = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Grade = table.Column<int>(type: "int", nullable: true),
                    OfferedGrade = table.Column<int>(type: "int", nullable: true),
                    DepartmentID = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Subjects", x => x.SubjectID);
                    table.ForeignKey(
                        name: "FK_Subjects_Departments_DepartmentID",
                        column: x => x.DepartmentID,
                        principalTable: "Departments",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateTable(
                name: "Teachers",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DepartmentID = table.Column<int>(type: "int", nullable: false),
                    IsPasswordChanged = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Teachers", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Teachers_Departments_DepartmentID",
                        column: x => x.DepartmentID,
                        principalTable: "Departments",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Trainings",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DepartmentID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Trainings", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Trainings_Departments_DepartmentID",
                        column: x => x.DepartmentID,
                        principalTable: "Departments",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TakenCourses",
                columns: table => new
                {
                    TakenCourseID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SubjectID = table.Column<int>(type: "int", nullable: true),
                    Grade = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TakenCourses", x => x.TakenCourseID);
                    table.ForeignKey(
                        name: "FK_TakenCourses_Subjects_SubjectID",
                        column: x => x.SubjectID,
                        principalTable: "Subjects",
                        principalColumn: "SubjectID");
                });

            migrationBuilder.CreateTable(
                name: "Tests",
                columns: table => new
                {
                    TestID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SubjectID = table.Column<int>(type: "int", nullable: false),
                    Result = table.Column<double>(type: "float", nullable: false),
                    StartDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CompletionDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TimeSpent = table.Column<int>(type: "int", nullable: false),
                    TimeLimit = table.Column<int>(type: "int", nullable: false),
                    IsCompleted = table.Column<bool>(type: "bit", nullable: false),
                    Type = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Label = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PreviousTestID = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tests", x => x.TestID);
                    table.ForeignKey(
                        name: "FK_Tests_Subjects_SubjectID",
                        column: x => x.SubjectID,
                        principalTable: "Subjects",
                        principalColumn: "SubjectID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SemesterTakenCourse",
                columns: table => new
                {
                    SemestersID = table.Column<int>(type: "int", nullable: false),
                    TakenCoursesTakenCourseID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SemesterTakenCourse", x => new { x.SemestersID, x.TakenCoursesTakenCourseID });
                    table.ForeignKey(
                        name: "FK_SemesterTakenCourse_Semesters_SemestersID",
                        column: x => x.SemestersID,
                        principalTable: "Semesters",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SemesterTakenCourse_TakenCourses_TakenCoursesTakenCourseID",
                        column: x => x.TakenCoursesTakenCourseID,
                        principalTable: "TakenCourses",
                        principalColumn: "TakenCourseID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "StudentTakenCourse",
                columns: table => new
                {
                    StudentsID = table.Column<int>(type: "int", nullable: false),
                    TakenCoursesTakenCourseID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StudentTakenCourse", x => new { x.StudentsID, x.TakenCoursesTakenCourseID });
                    table.ForeignKey(
                        name: "FK_StudentTakenCourse_Students_StudentsID",
                        column: x => x.StudentsID,
                        principalTable: "Students",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_StudentTakenCourse_TakenCourses_TakenCoursesTakenCourseID",
                        column: x => x.TakenCoursesTakenCourseID,
                        principalTable: "TakenCourses",
                        principalColumn: "TakenCourseID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TakenCourseTeacher",
                columns: table => new
                {
                    TakenCoursesTakenCourseID = table.Column<int>(type: "int", nullable: false),
                    TeachersID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TakenCourseTeacher", x => new { x.TakenCoursesTakenCourseID, x.TeachersID });
                    table.ForeignKey(
                        name: "FK_TakenCourseTeacher_TakenCourses_TakenCoursesTakenCourseID",
                        column: x => x.TakenCoursesTakenCourseID,
                        principalTable: "TakenCourses",
                        principalColumn: "TakenCourseID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TakenCourseTeacher_Teachers_TeachersID",
                        column: x => x.TeachersID,
                        principalTable: "Teachers",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "StudentTest",
                columns: table => new
                {
                    StudentsID = table.Column<int>(type: "int", nullable: false),
                    TestsTestID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StudentTest", x => new { x.StudentsID, x.TestsTestID });
                    table.ForeignKey(
                        name: "FK_StudentTest_Students_StudentsID",
                        column: x => x.StudentsID,
                        principalTable: "Students",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_StudentTest_Tests_TestsTestID",
                        column: x => x.TestsTestID,
                        principalTable: "Tests",
                        principalColumn: "TestID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_SemesterTakenCourse_TakenCoursesTakenCourseID",
                table: "SemesterTakenCourse",
                column: "TakenCoursesTakenCourseID");

            migrationBuilder.CreateIndex(
                name: "IX_StudentTakenCourse_TakenCoursesTakenCourseID",
                table: "StudentTakenCourse",
                column: "TakenCoursesTakenCourseID");

            migrationBuilder.CreateIndex(
                name: "IX_StudentTest_TestsTestID",
                table: "StudentTest",
                column: "TestsTestID");

            migrationBuilder.CreateIndex(
                name: "IX_Subjects_DepartmentID",
                table: "Subjects",
                column: "DepartmentID");

            migrationBuilder.CreateIndex(
                name: "IX_TakenCourses_SubjectID",
                table: "TakenCourses",
                column: "SubjectID");

            migrationBuilder.CreateIndex(
                name: "IX_TakenCourseTeacher_TeachersID",
                table: "TakenCourseTeacher",
                column: "TeachersID");

            migrationBuilder.CreateIndex(
                name: "IX_Teachers_DepartmentID",
                table: "Teachers",
                column: "DepartmentID");

            migrationBuilder.CreateIndex(
                name: "IX_Tests_SubjectID",
                table: "Tests",
                column: "SubjectID");

            migrationBuilder.CreateIndex(
                name: "IX_Trainings_DepartmentID",
                table: "Trainings",
                column: "DepartmentID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SemesterTakenCourse");

            migrationBuilder.DropTable(
                name: "StudentTakenCourse");

            migrationBuilder.DropTable(
                name: "StudentTest");

            migrationBuilder.DropTable(
                name: "TakenCourseTeacher");

            migrationBuilder.DropTable(
                name: "Trainings");

            migrationBuilder.DropTable(
                name: "Semesters");

            migrationBuilder.DropTable(
                name: "Students");

            migrationBuilder.DropTable(
                name: "Tests");

            migrationBuilder.DropTable(
                name: "TakenCourses");

            migrationBuilder.DropTable(
                name: "Teachers");

            migrationBuilder.DropTable(
                name: "Subjects");

            migrationBuilder.DropTable(
                name: "Departments");
        }
    }
}
