using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using TestApp.DataAccess.InitializationDb;
using TestApp.DataAccess.Repositories.Interfaces;
using TestApp.Domain.Models;

namespace TestApp.DataAccess.Context
{
    public class TestAppContext : DbContext, IUnitOfWork
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Test> Tests { get; set; }
        public DbSet<Question> Questions { get; set; }
        public DbSet<Answer> Answers { get; set; }
        public DbSet<UserAnswer> UserAnswers { get; set; }


        public TestAppContext(DbContextOptions<TestAppContext> options) : base(options)
        {
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
                t.HasMany(t => t.Questions)
                 .WithMany(q => q.Tests)
                 .UsingEntity(qt => qt.ToTable("QuestionTest"));
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

            // Initialization DB.
            modelBuilder.Seed();
        }
    }
}
