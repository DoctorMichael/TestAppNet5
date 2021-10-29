using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestApp.Domain.Models;

namespace TestApp.DataAccess.InitializationDb
{
    public static class SeedData
    {
        public static void Seed(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().HasData(
                new() { Id = 1, Name = "Mike", Password = "1111", IsController = true },
                new() { Id = 2, Name = "Ann", Password = "2222", IsController = false }
                );


            modelBuilder.Entity<Test>().HasData(
                new() { Id = 1, TestName = "Mathematics" },
                new() { Id = 2, TestName = "English" },
                new() { Id = 3, TestName = "Pretend To Be Kind" }
                );


            modelBuilder.Entity<Question>().HasData(
                new() { Id = 1, QuestionText = "The Nearest Result for 2 x 2 = ..." },
                new() { Id = 2, QuestionText = "0 / 0 = ..." },
                new() { Id = 3, QuestionText = "(-1)^(1 / 2) = ..." },

                new() { Id = 4, QuestionText = "How Old Are You?" },
                new() { Id = 5, QuestionText = "One  Two  ...  Four" },

                new() { Id = 6, QuestionText = "Are You Sorry For Moo-moo?" }
                );


            modelBuilder.Entity<Answer>().HasData(
                new() { Id = 1, IsCorrect = true, AnswerText = "3", QuestionId = 1 },
                new() { Id = 2, IsCorrect = false, AnswerText = "7...8", QuestionId = 1 },
                new() { Id = 3, IsCorrect = false, AnswerText = "22", QuestionId = 1 },

                new() { Id = 4, IsCorrect = false, AnswerText = "1", QuestionId = 2 },
                new() { Id = 5, IsCorrect = false, AnswerText = "0", QuestionId = 2 },
                new() { Id = 6, IsCorrect = false, AnswerText = "inf.", QuestionId = 2 },
                new() { Id = 7, IsCorrect = true, AnswerText = "There is No Correct Answer.", QuestionId = 2 },
                new() { Id = 8, IsCorrect = false, AnswerText = "-inf.", QuestionId = 2 },

                new() { Id = 9, IsCorrect = false, AnswerText = "Ой!", QuestionId = 3 },
                new() { Id = 10, IsCorrect = false, AnswerText = "-1", QuestionId = 3 },
                new() { Id = 11, IsCorrect = false, AnswerText = "И", QuestionId = 3 },
                new() { Id = 12, IsCorrect = true, AnswerText = "i", QuestionId = 3 },

                //----------------------------------------------------

                new() { Id = 13, IsCorrect = false, AnswerText = "Yes, I am.", QuestionId = 4 },
                new() { Id = 14, IsCorrect = false, AnswerText = "London.", QuestionId = 4 },
                new() { Id = 15, IsCorrect = false, AnswerText = "Ich spreche kein Deutsch.", QuestionId = 4 },
                new() { Id = 16, IsCorrect = true, AnswerText = "There is No Correct Answer.", QuestionId = 4 },

                new() { Id = 17, IsCorrect = true, AnswerText = "3", QuestionId = 5 },
                new() { Id = 18, IsCorrect = false, AnswerText = "3.14", QuestionId = 5 },
                new() { Id = 19, IsCorrect = true, AnswerText = "Three", QuestionId = 5 },
                new() { Id = 20, IsCorrect = false, AnswerText = "WTF?", QuestionId = 5 },

                //----------------------------------------------------

                new() { Id = 21, IsCorrect = true, AnswerText = "Yes", QuestionId = 6 },
                new() { Id = 22, IsCorrect = false, AnswerText = "No", QuestionId = 6 }
                );
        }
    }
}
