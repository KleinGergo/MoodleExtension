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
    [Migration("20231026125553_Database")]
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

                    b.Property<int>("TeacherID")
                        .HasColumnType("int");

                    b.Property<int>("TrainingID")
                        .HasColumnType("int");

                    b.HasKey("ID");

                    b.HasIndex("TeacherID");

                    b.HasIndex("TrainingID");

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

                    b.Property<string>("DepartmentID")
                        .HasColumnType("nvarchar(max)");

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

                    b.ToTable("Subjects");
                });

            modelBuilder.Entity("MoodleExtensionAPI.Models.TakenCourse", b =>
                {
                    b.Property<int>("TakenCourseID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("TakenCourseID"));

                    b.Property<string>("Neptuncode")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("SemesterID")
                        .HasColumnType("int");

                    b.Property<string>("StudentNeptuncode")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("SubjectID")
                        .HasColumnType("int");

                    b.Property<int>("TeacherID")
                        .HasColumnType("int");

                    b.HasKey("TakenCourseID");

                    b.HasIndex("SemesterID");

                    b.HasIndex("StudentNeptuncode");

                    b.HasIndex("SubjectID");

                    b.HasIndex("TeacherID");

                    b.ToTable("TakenCourses");
                });

            modelBuilder.Entity("MoodleExtensionAPI.Models.Teacher", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ID"));

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

                    b.Property<string>("Neptuncode")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("PreviousTestID")
                        .HasColumnType("int");

                    b.Property<double>("Result")
                        .HasColumnType("float");

                    b.Property<DateTime>("StartDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("StudentNeptuncode")
                        .HasColumnType("nvarchar(450)");

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

                    b.HasIndex("StudentNeptuncode");

                    b.HasIndex("SubjectID");

                    b.ToTable("Tests");
                });

            modelBuilder.Entity("MoodleExtensionAPI.Models.Training", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ID"));

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ID");

                    b.ToTable("Trainings");
                });

            modelBuilder.Entity("MoodleExtensionAPI.Models.Department", b =>
                {
                    b.HasOne("MoodleExtensionAPI.Models.Teacher", "Teacher")
                        .WithMany("DepartmentID")
                        .HasForeignKey("TeacherID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("MoodleExtensionAPI.Models.Training", "Training")
                        .WithMany("DepartmentID")
                        .HasForeignKey("TrainingID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Teacher");

                    b.Navigation("Training");
                });

            modelBuilder.Entity("MoodleExtensionAPI.Models.TakenCourse", b =>
                {
                    b.HasOne("MoodleExtensionAPI.Models.Semester", "Semester")
                        .WithMany("TakenCourses")
                        .HasForeignKey("SemesterID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("MoodleExtensionAPI.Models.Student", "Student")
                        .WithMany("TakenCourses")
                        .HasForeignKey("StudentNeptuncode");

                    b.HasOne("MoodleExtensionAPI.Models.Subject", "Subject")
                        .WithMany()
                        .HasForeignKey("SubjectID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("MoodleExtensionAPI.Models.Teacher", "Teacher")
                        .WithMany("TakenCourses")
                        .HasForeignKey("TeacherID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Semester");

                    b.Navigation("Student");

                    b.Navigation("Subject");

                    b.Navigation("Teacher");
                });

            modelBuilder.Entity("MoodleExtensionAPI.Models.Test", b =>
                {
                    b.HasOne("MoodleExtensionAPI.Models.Student", "Student")
                        .WithMany("Tests")
                        .HasForeignKey("StudentNeptuncode");

                    b.HasOne("MoodleExtensionAPI.Models.Subject", "Subject")
                        .WithMany("Tests")
                        .HasForeignKey("SubjectID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Student");

                    b.Navigation("Subject");
                });

            modelBuilder.Entity("MoodleExtensionAPI.Models.Semester", b =>
                {
                    b.Navigation("TakenCourses");
                });

            modelBuilder.Entity("MoodleExtensionAPI.Models.Student", b =>
                {
                    b.Navigation("TakenCourses");

                    b.Navigation("Tests");
                });

            modelBuilder.Entity("MoodleExtensionAPI.Models.Subject", b =>
                {
                    b.Navigation("Tests");
                });

            modelBuilder.Entity("MoodleExtensionAPI.Models.Teacher", b =>
                {
                    b.Navigation("DepartmentID");

                    b.Navigation("TakenCourses");
                });

            modelBuilder.Entity("MoodleExtensionAPI.Models.Training", b =>
                {
                    b.Navigation("DepartmentID");
                });
#pragma warning restore 612, 618
        }
    }
}
