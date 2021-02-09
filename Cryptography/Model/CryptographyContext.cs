using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Cryptography.Model
{
    class CryptographyContext : DbContext
    {
        public DbSet<Student> Students { get; set; }
        public DbSet<Teacher> Teachers { get; set; }
        public DbSet<TeacherCipher> TeacherCiphers { get; set; }
        public DbSet<StudentCipher> StudentCiphers { get; set; }

        public CryptographyContext()
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=.\\SQLEXPRESS;Database=CryptographyDB;Trusted_Connection=True");
            //optionsBuilder.UseMySQL("Server=localhost; Port=3306; Database=TestContext; User=user; Password=password");
            optionsBuilder.EnableSensitiveDataLogging();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //modelBuilder.Entity<StudentGameQuiz>().HasKey(sgq => new { sgq.StudentId, sgq.GameQuizId });
        }
    }
}
