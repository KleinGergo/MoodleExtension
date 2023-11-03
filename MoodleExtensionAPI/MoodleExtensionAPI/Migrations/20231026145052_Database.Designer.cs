﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using MoodleExtensionAPI.Data;

#nullable disable

namespace MoodleExtensionAPI.Migrations
{
    [DbContext(typeof(Context))]
    [Migration("20231026145052_Database")]
    partial class Database
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.10")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("MoodleExtensionAPI.Models.Department", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ID"));

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ID");

                    b.ToTable("Departments");
                });

            modelBuilder.Entity("MoodleExtensionAPI.Models.Semester", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ID"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Year")
                        .HasColumnType("int");

                    b.HasKey("ID");

                    b.ToTable("Semesters");
                });

            modelBuilder.Entity("MoodleExtensionAPI.Models.Student", b =>
                {
                    b.Property<string>("Neptuncode")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FirstName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LastName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Training")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Neptuncode");

                    b.ToTable("Students");
                });

            modelBuilder.Entity("MoodleExtensionAPI.Models.Subject", b =>
                {
                    b.Property<int>("SubjectID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("SubjectID"));

                    b.Property<int>("DepartmentID")
                        .HasColumnType("int");

                    b.Property<int?>("Grade")
                        .HasColumnType("int");

                    b.Property<int?>("OfferedGrade")
                        .HasColumnType("int");

                    b.Property<string>("SignatureCondition")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("SignatureState")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("SubjectName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("numberOfTests")
                        .HasColumnType("int");

                    b.HasKey("SubjectID");

                    b.HasIndex("DepartmentID");

                    b.ToTable("Subjects");
                });

            modelBuilder.Entity("MoodleExtensionAPI.Models.TakenCourse", b =>
                {
                    b.Property<int>("TakenCourseID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("TakenCourseID"));

                    b.Property<int>("Grade")
                        .HasColumnType("int");

                    b.Property<int?>("SubjectID")
                        .HasColumnType("int");

                    b.HasKey("TakenCourseID");

                    b.HasIndex("SubjectID");

                    b.ToTable("TakenCourses");
                });

            modelBuilder.Entity("MoodleExtensionAPI.Models.Teacher", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ID"));

                    b.Property<int>("DepartmentID")
                        .HasColumnType("int");

                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsPasswordChanged")
                        .HasColumnType("bit");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PasswordDb")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("Password");

                    b.HasKey("ID");

                    b.HasIndex("DepartmentID");

                    b.ToTable("Teachers");
                });

            modelBuilder.Entity("MoodleExtensionAPI.Models.Test", b =>
                {
                    b.Property<int>("TestID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("TestID"));

                    b.Property<DateTime>("CompletionDate")
                        .HasColumnType("datetime2");

                    b.Property<bool>("IsCompleted")
                        .HasColumnType("bit");

                    b.Property<string>("Label")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("PreviousTestID")
                        .HasColumnType("int");

                    b.Property<double>("Result")
                        .HasColumnType("float");

                    b.Property<DateTime>("StartDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("SubjectID")
                        .HasColumnType("int");

                    b.Property<int>("TimeLimit")
                        .HasColumnType("int");

                    b.Property<int>("TimeSpent")
                        .HasColumnType("int");

                    b.Property<string>("Type")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("TestID");

                    b.HasIndex("SubjectID");

                    b.ToTable("Tests");
                });

            modelBuilder.Entity("MoodleExtensionAPI.Models.Training", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ID"));

                    b.Property<int>("DepartmentID")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ID");

                    b.HasIndex("DepartmentID");

                    b.ToTable("Trainings");
                });

            modelBuilder.Entity("SemesterTakenCourse", b =>
                {
                    b.Property<int>("SemestersID")
                        .HasColumnType("int");

                    b.Property<int>("TakenCoursesTakenCourseID")
                        .HasColumnType("int");

                    b.HasKey("SemestersID", "TakenCoursesTakenCourseID");

                    b.HasIndex("TakenCoursesTakenCourseID");

                    b.ToTable("SemesterTakenCourse");
                });

            modelBuilder.Entity("StudentTakenCourse", b =>
                {
                    b.Property<string>("StudentsNeptuncode")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("TakenCoursesTakenCourseID")
                        .HasColumnType("int");

                    b.HasKey("StudentsNeptuncode", "TakenCoursesTakenCourseID");

                    b.HasIndex("TakenCoursesTakenCourseID");

                    b.ToTable("StudentTakenCourse");
                });

            modelBuilder.Entity("StudentTest", b =>
                {
                    b.Property<string>("StudentsNeptuncode")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("TestsTestID")
                        .HasColumnType("int");

                    b.HasKey("StudentsNeptuncode", "TestsTestID");

                    b.HasIndex("TestsTestID");

                    b.ToTable("StudentTest");
                });

            modelBuilder.Entity("TakenCourseTeacher", b =>
                {
                    b.Property<int>("TakenCoursesTakenCourseID")
                        .HasColumnType("int");

                    b.Property<int>("TeachersID")
                        .HasColumnType("int");

                    b.HasKey("TakenCoursesTakenCourseID", "TeachersID");

                    b.HasIndex("TeachersID");

                    b.ToTable("TakenCourseTeacher");
                });

            modelBuilder.Entity("MoodleExtensionAPI.Models.Subject", b =>
                {
                    b.HasOne("MoodleExtensionAPI.Models.Department", "Department")
                        .WithMany()
                        .HasForeignKey("DepartmentID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Department");
                });

            modelBuilder.Entity("MoodleExtensionAPI.Models.TakenCourse", b =>
                {
                    b.HasOne("MoodleExtensionAPI.Models.Subject", "Subject")
                        .WithMany()
                        .HasForeignKey("SubjectID");

                    b.Navigation("Subject");
                });

            modelBuilder.Entity("MoodleExtensionAPI.Models.Teacher", b =>
                {
                    b.HasOne("MoodleExtensionAPI.Models.Department", "Department")
                        .WithMany("Teachers")
                        .HasForeignKey("DepartmentID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Department");
                });

            modelBuilder.Entity("MoodleExtensionAPI.Models.Test", b =>
                {
                    b.HasOne("MoodleExtensionAPI.Models.Subject", "Subject")
                        .WithMany("Tests")
                        .HasForeignKey("SubjectID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Subject");
                });

            modelBuilder.Entity("MoodleExtensionAPI.Models.Training", b =>
                {
                    b.HasOne("MoodleExtensionAPI.Models.Department", "Department")
                        .WithMany("Trainings")
                        .HasForeignKey("DepartmentID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Department");
                });

            modelBuilder.Entity("SemesterTakenCourse", b =>
                {
                    b.HasOne("MoodleExtensionAPI.Models.Semester", null)
                        .WithMany()
                        .HasForeignKey("SemestersID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("MoodleExtensionAPI.Models.TakenCourse", null)
                        .WithMany()
                        .HasForeignKey("TakenCoursesTakenCourseID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("StudentTakenCourse", b =>
                {
                    b.HasOne("MoodleExtensionAPI.Models.Student", null)
                        .WithMany()
                        .HasForeignKey("StudentsNeptuncode")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("MoodleExtensionAPI.Models.TakenCourse", null)
                        .WithMany()
                        .HasForeignKey("TakenCoursesTakenCourseID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("StudentTest", b =>
                {
                    b.HasOne("MoodleExtensionAPI.Models.Student", null)
                        .WithMany()
                        .HasForeignKey("StudentsNeptuncode")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("MoodleExtensionAPI.Models.Test", null)
                        .WithMany()
                        .HasForeignKey("TestsTestID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("TakenCourseTeacher", b =>
                {
                    b.HasOne("MoodleExtensionAPI.Models.TakenCourse", null)
                        .WithMany()
                        .HasForeignKey("TakenCoursesTakenCourseID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("MoodleExtensionAPI.Models.Teacher", null)
                        .WithMany()
                        .HasForeignKey("TeachersID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("MoodleExtensionAPI.Models.Department", b =>
                {
                    b.Navigation("Teachers");

                    b.Navigation("Trainings");
                });

            modelBuilder.Entity("MoodleExtensionAPI.Models.Subject", b =>
                {
                    b.Navigation("Tests");
                });
#pragma warning restore 612, 618
        }
    }
}
