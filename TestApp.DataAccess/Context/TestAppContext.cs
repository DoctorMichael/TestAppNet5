using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestApp.Domain.Models;

namespace TestApp.DataAccess.Context
{
    public class TestAppContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Test> Tests { get; set; }
        public DbSet<Question> Questions { get; set; }
        public DbSet<Answer> Answers { get; set; }
        public DbSet<UserAnswer> UserAnswers { get; set; }


        public TestAppContext(DbContextOptions<TestAppContext> options) : base(options)
        {
        }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=TestDb;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");
        }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>(u =>
            {
                u.HasKey(x => x.Id);
                u.Property(x => x.Name).IsRequired();
                u.Property(x => x.Password).IsRequired(); ;
                u.Property(x => x.IsController).IsRequired();
            });

            modelBuilder.Entity<Test>(t =>
            {
                t.HasKey(x => x.Id);
                t.HasAlternateKey(x => x.TestName);
            });

            modelBuilder.Entity<Question>(q =>
            {
                q.HasKey(x => x.Id);
                q.Property(x => x.QuestionText).IsRequired();
            });

            modelBuilder.Entity<Answer>(a =>
            {
                a.HasKey(x => x.Id);
                a.Property(x => x.AnswerText).IsRequired();
                a.Property(x => x.IsCorrect).IsRequired();
                a.HasOne(x => x.Question)
                 .WithMany(q => q.Answers)
                 .HasForeignKey(x => x.QuestionId);
            });

            modelBuilder.Entity<UserAnswer>().HasKey(ua => new { ua.UserID, ua.TestID, ua.AnswerID });

        }
    }
}
