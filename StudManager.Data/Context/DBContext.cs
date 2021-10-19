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
        public DbSet<Subject> Subjects { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
           
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Fees>()
                .HasOne(f => f.Student)
                .WithOne()
                .HasForeignKey<Fees>(s => s.StuId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Fees>()
                .HasOne(f => f.Course)
                .WithOne()
                .HasForeignKey<Fees>(s => s.CourseId)
                .OnDelete(DeleteBehavior.Restrict);

            //modelBuilder.Entity<StudentExam>().HasKey(se => new { se.ExamId, se.StudentId });

            //modelBuilder.Entity<StudentExam>()
            //    .HasOne<Student>(sc => sc.Student)
            //    .WithMany(s => s.StudentExams)
            //    .HasForeignKey(sc => sc.StudentId);

            //modelBuilder.Entity<StudentExam>()
            //    .HasOne<Exam>(sc => sc.Exam)
            //    .WithMany(s => s.StudentExams)
            //    .HasForeignKey(sc => sc.ExamId);

        }

    }
}
