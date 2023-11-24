using Microsoft.EntityFrameworkCore;
using MoodleExtensionAPI.Models;

namespace MoodleExtensionAPI.Data
{
    public class Context : DbContext
    {

        public DbSet<Semester> Semesters { get; set; } = null!;
        public DbSet<Student> Students { get; set; } = null!;
        public DbSet<Subject> Subjects { get; set; } = null!;
        public DbSet<TakenCourse> TakenCourses { get; set; } = null!;
        public DbSet<Teacher> Teachers { get; set; } = null!;
        public DbSet<Test> Tests { get; set; } = null!;
        public DbSet<Training> Trainings { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=(localdb)\\MSSQLLocalDB;Database=Stats;Trusted_Connection=True;");

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {


        }
    }
}
