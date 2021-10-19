using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
                new() { IsCorrect = true, AnswerText = "3" },
                new() { IsCorrect = false, AnswerText = "7...8" },
                new() { IsCorrect = false, AnswerText = "22" },

                new() { IsCorrect = false, AnswerText = "1" },
                new() { IsCorrect = false, AnswerText = "0" },
                new() { IsCorrect = false, AnswerText = "inf." },
                new() { IsCorrect = true, AnswerText = "There is No Correct Answer." },
                new() { IsCorrect = false, AnswerText = "-inf." },

                new() { IsCorrect = false, AnswerText = "Ой!" },
                new() { IsCorrect = false, AnswerText = "-1" },
                new() { IsCorrect = false, AnswerText = "И" },
                new() { IsCorrect = true, AnswerText = "i" },

                //----------------------------------------------------

                new() { IsCorrect = false, AnswerText = "Yes, I am." },
                new() { IsCorrect = false, AnswerText = "London." },
                new() { IsCorrect = false, AnswerText = "Ich spreche kein Deutsch." },
                new() { IsCorrect = true, AnswerText = "There is No Correct Answer." },

                new() { IsCorrect = true, AnswerText = "3" },
                new() { IsCorrect = false, AnswerText = "3.14" },
                new() { IsCorrect = true, AnswerText = "Three" },
                new() { IsCorrect = false, AnswerText = "WTF?" },

                //----------------------------------------------------

                new() { IsCorrect = true, AnswerText = "Yes" },
                new() { IsCorrect = false, AnswerText = "No" }
            };

            List<Question> questionTable = new()
            {
                new (){ QuestionText = "The Nearest Result for 2 x 2 = ...", Answers = new List<Answer>{ answerTable[0], answerTable[1], answerTable[2] } },
                new (){ QuestionText = "0 / 0 = ...", Answers = new List<Answer>{ answerTable[3], answerTable[4], answerTable[5], answerTable[6], answerTable[7] } },
                new (){ QuestionText = "(-1)^(1 / 2) = ...", Answers = new List<Answer>{ answerTable[8], answerTable[9], answerTable[10], answerTable[11] } },

                new (){ QuestionText = "How Old Are You?", Answers = new List<Answer>{ answerTable[12], answerTable[13], answerTable[14], answerTable[15] } },
                new (){ QuestionText = "One  Two  ...  Four", Answers = new List<Answer>{ answerTable[16], answerTable[17], answerTable[18], answerTable[19] } },

                new (){ QuestionText = "Are You Sorry For Moo-moo?", Answers = new List<Answer>{ answerTable[20], answerTable[21] } }
            };

            List<Test> testTable = new()
            {
                new (){ TestName = "Mathematics", Questions = new List<Question>{ questionTable[0], questionTable[1], questionTable[2] } },
                new (){ TestName = "English", Questions = new List<Question>{ questionTable[3], questionTable[4] } },
                new (){ TestName = "Pretend To Be Kind", Questions = new List<Question>{ questionTable[5] } }
            };


            context.Users.AddRange(userTable);
            context.SaveChanges();
     
            context.Tests.AddRange(testTable);
            context.SaveChanges();
        }
    }
}
