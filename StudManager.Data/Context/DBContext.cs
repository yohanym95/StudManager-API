using Microsoft.EntityFrameworkCore;
using StudManager.Data.Data.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace StudManager.Data.Context
{
    public class DBContext : DbContext
    {
        public DBContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Student> Students { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Student>().HasData(new Student()
            {
                FirstName ="Yohan",
                LastName = "Malshika",
                FullName = "Yohan Malshika",
                BirthDate ="1995/09/30",
                PhoneNumber = "00000000"
            });
        }

    }
}
