using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestApp.BusinessLogic.Services.Interfaces;
using TestApp.DataAccess.Context;
using TestApp.Domain.Models;

namespace TestApp.BusinessLogic.Services.Implementation
{
    public class UserService //: IUserService
    {
        public async Task<Answer> GetLastAnswerAsync()
        {
            using TestAppContext cntx = new();
            return await cntx.Answers.OrderBy(a => a.Id).LastAsync();
        }


        public async Task<List<User>> GetAllUsersAsync(bool inclUserAnswers)
        {
            using TestAppContext cntx = new();

            if (inclUserAnswers)
                return await cntx.Users.AsNoTracking()
                                       .Include(u => u.UserAnswers)
                                       .ToListAsync();
            else
                return await cntx.Users.AsNoTracking()
                                       .ToListAsync();
        }


        public async Task<List<Test>> GetAllTestsAsync(bool inclQuestions)
        {
            using TestAppContext cntx = new();

            if (inclQuestions)
                return await cntx.Tests.AsNoTracking()
                                       .Include(t => t.Questions)
                                       .ThenInclude(q => q.Answers)
                                       .ToListAsync();
            else
                return await cntx.Tests.AsNoTracking()
                                       .ToListAsync();
        }


        public async Task<Test> GetSingleTestAsync(Test test)
        {
            using TestAppContext cntx = new();

            return await cntx.Tests.Include(t => t.Questions)
                                   .ThenInclude(q => q.Answers)
                                   .FirstOrDefaultAsync(t => t.Id == test.Id || t.TestName == test.TestName);
        }


        public async Task<Question> GetSingleQuestionAsync(Question question)
        {
            using TestAppContext cntx = new();

            return await cntx.Questions.Include(q => q.Answers)
                                       .FirstOrDefaultAsync(q => q.Id == question.Id || q.QuestionText == question.QuestionText);
        }


        public async Task AddNewTestAsync(Test test)
        {
            using TestAppContext cntx = new();

            Task task = null;

            task = Task.Run(() =>
            {
                cntx.Tests.Add(test);
                cntx.SaveChanges();
            });

            await task;
        }


        public async Task UpdateTestAsync(Test test)
        {
            using TestAppContext cntx = new();

            Task task = null;

            task = Task.Run(() =>
            {
                cntx.Tests.Update(test);
                cntx.SaveChanges();
            });

            await task;
        }

        public async Task RemoveTestAsync(Test test)
        {
            using TestAppContext cntx = new();

            await Task.Run(() =>
            {
                var testList = cntx.Tests.Where(t => t.Id == test.Id || t.TestName == test.TestName).ToList();
                cntx.Tests.RemoveRange(testList);
                cntx.SaveChanges();
            });
        }

        //async Task<List<Question>> GetTestQuestions(Test test)
        //{
        //    using (TestAppContext cntx = new TestAppContext())
        //    {


        //var users = await cntx.Users
        //                         //.Include(p => p.Name)
        //                         //.Where(p => p.CompanyId == 1)
        //                         .ToListAsync()
        //                         ;     // асинхронное получение данных

        //        var highScores = from q in test.Questions
        //                         where student.ExamScores[exam] > score
        //                         select new { Name = student.FirstName, Score = student.ExamScores[exam] };

        //        //var users = await cntx.Tests.FirstOrDefault(t => (t.Id == test.Id || t.TestName == test.TestName));

        //        //return await cntx.Questions.Include(q => q.Tests.FirstOrDefault(p => p.Id == test.Id)).ToListAsync();
        //        return await cntx.Tests.Where(t => t.Id == test.Id).Any(car => car.Questions.).ToListAsync();
        //    }
        //}


    }
}
