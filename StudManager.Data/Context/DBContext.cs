using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using StudManager.Data.Data.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace StudManager.Data.Context
{
    public class DBContext : IdentityDbContext<ApplicationUser>
    {
        public DBContext()
        {

        }
        public DBContext(DbContextOptions options) : base(options)
        {
        }
        public DbSet<Student> Students { get; set; }
        public DbSet<Course> Courses { get; set; }
        public DbSet<Fees> Fees { get; set; }

        public DbSet<Exam> Exams { get; set; }
        public DbSet<StudentExam> StudentExams { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
           
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<StudentExam>().HasKey(se => new { se.ExamId, se.StudentId });

            modelBuilder.Entity<StudentExam>()
                .HasOne<Student>(sc => sc.Student)
                .WithMany(s => s.StudentExams)
                .HasForeignKey(sc => sc.StudentId);

            modelBuilder.Entity<StudentExam>()
                .HasOne<Exam>(sc => sc.Exam)
                .WithMany(s => s.StudentExams)
                .HasForeignKey(sc => sc.ExamId);

        }

    }
}
