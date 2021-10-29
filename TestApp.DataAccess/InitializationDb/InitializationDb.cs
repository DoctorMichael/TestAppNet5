using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using TestApp.DataAccess.Context;
using TestApp.Domain.Models;

namespace TestApp.DataAccess.InitializationDb
{
    public class InitializationDb
    {
        public InitializationDb()
        {
            TestAppContext context = new TestAppContext(new DbContextOptions<TestAppContext>());

            List<User> userTable = new()
            {
                new() { Name = "Mike", Password = "1111", IsController = true },
                new() { Name = "Ann", Password = "2222", IsController = false }
            };

            List<Answer> answerTable = new()
            {
                new() { IsCorrect = true, AnswerText = "3", QuestionId = 1 },
                new() { IsCorrect = false, AnswerText = "7...8", QuestionId = 1 },
                new() { IsCorrect = false, AnswerText = "22", QuestionId = 1 },

                new() { IsCorrect = false, AnswerText = "1", QuestionId = 2 },
                new() { IsCorrect = false, AnswerText = "0", QuestionId = 2 },
                new() { IsCorrect = false, AnswerText = "inf.", QuestionId = 2 },
                new() { IsCorrect = true, AnswerText = "There is No Correct Answer.", QuestionId = 2 },
                new() { IsCorrect = false, AnswerText = "-inf.", QuestionId = 2 },

                new() { IsCorrect = false, AnswerText = "Ой!", QuestionId = 3 },
                new() { IsCorrect = false, AnswerText = "-1", QuestionId = 3 },
                new() { IsCorrect = false, AnswerText = "И", QuestionId = 3 },
                new() { IsCorrect = true, AnswerText = "i", QuestionId = 3 },

                //----------------------------------------------------

                new() { IsCorrect = false, AnswerText = "Yes, I am.", QuestionId = 4 },
                new() { IsCorrect = false, AnswerText = "London.", QuestionId = 4 },
                new() { IsCorrect = false, AnswerText = "Ich spreche kein Deutsch.", QuestionId = 4 },
                new() { IsCorrect = true, AnswerText = "There is No Correct Answer.", QuestionId = 4 },

                new() { IsCorrect = true, AnswerText = "3", QuestionId = 5 },
                new() { IsCorrect = false, AnswerText = "3.14", QuestionId = 5 },
                new() { IsCorrect = true, AnswerText = "Three", QuestionId = 5 },
                new() { IsCorrect = false, AnswerText = "WTF?", QuestionId = 5 },

                //----------------------------------------------------

                new() { IsCorrect = true, AnswerText = "Yes", QuestionId = 6 },
                new() { IsCorrect = false, AnswerText = "No", QuestionId = 6 }
            };

            List<Question> questionTable = new()
            {
                new() { QuestionText = "The Nearest Result for 2 x 2 = ...", Answers = new List<Answer> { answerTable[0], answerTable[1], answerTable[2] } },
                new() { QuestionText = "0 / 0 = ...", Answers = new List<Answer> { answerTable[3], answerTable[4], answerTable[5], answerTable[6], answerTable[7] } },
                new() { QuestionText = "(-1)^(1 / 2) = ...", Answers = new List<Answer> { answerTable[8], answerTable[9], answerTable[10], answerTable[11] } },

                new() { QuestionText = "How Old Are You?", Answers = new List<Answer> { answerTable[12], answerTable[13], answerTable[14], answerTable[15] } },
                new() { QuestionText = "One  Two  ...  Four", Answers = new List<Answer> { answerTable[16], answerTable[17], answerTable[18], answerTable[19] } },

                new() { QuestionText = "Are You Sorry For Moo-moo?", Answers = new List<Answer> { answerTable[20], answerTable[21] } }
            };

            List<Test> testTable = new()
            {
                new() { TestName = "Mathematics", Questions = new List<Question> { questionTable[0], questionTable[1], questionTable[2] } },
                new() { TestName = "English", Questions = new List<Question> { questionTable[3], questionTable[4] } },
                new() { TestName = "Pretend To Be Kind", Questions = new List<Question> { questionTable[5] } }
            };


            context.Users.AddRange(userTable);
            context.SaveChanges();

            context.Tests.AddRange(testTable);
            context.SaveChanges();
        }
    }
}
