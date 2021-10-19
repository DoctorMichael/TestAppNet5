using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestApp.BusinessLogic.Services.Interfaces;
using TestApp.DataAccess.Context;
using TestApp.DataAccess.Repositories.Interfaces;
using TestApp.Domain.Models;

namespace TestApp.BusinessLogic.Services.Implementation
{
    public class UserService : IUserService
    {
        readonly TestAppContext context;

        public UserService(IBaseRepository baseRepository)
        {
            context = baseRepository.Context;
        }


        public async Task<List<User>> GetAllUsersAsync(bool includeUserAnswers)
        {

            if (includeUserAnswers)
                return await context.Users.AsNoTracking()
                                       .Include(u => u.UserAnswers)
                                       .ToListAsync();
            else
                return await context.Users.AsNoTracking()
                                       .ToListAsync();
        }

        public async Task<List<Test>> GetAllTestsAsync(bool includeQuestions)
        {

            if (includeQuestions)
                return await context.Tests.AsNoTracking()
                                       .Include(t => t.Questions)
                                       .ThenInclude(q => q.Answers)
                                       .ToListAsync();
            else
                return await context.Tests.AsNoTracking()
                                       .ToListAsync();
        }

        public async Task<Test> GetSingleTestAsync(int testId)
        {
            return await context.Tests.Include(t => t.Questions)
                                   .ThenInclude(q => q.Answers)
                                   .FirstOrDefaultAsync(t => t.Id == testId);
        }

        public async Task<Question> GetSingleQuestionAsync(int questionId)
        {

            return await context.Questions.Include(q => q.Answers)
                                       .FirstOrDefaultAsync(q => q.Id == questionId);
        }

        public async Task AddNewTestAsync(Test test)
        {
            await context.Tests.AddAsync(test);
            await context.SaveChangesAsync();
        }

        public async Task UpdateTestAsync(Test test)
        {
            context.Tests.Update(test);
            await context.SaveChangesAsync();
        }

        public async Task RemoveTestAsync(int testId)
        { 
            var test = await context.Tests.Where(t => t.Id == testId).FirstOrDefaultAsync();
           
            if (test != null)
            {
                context.Tests.Remove(test);
                await context.SaveChangesAsync();
            }
        }
    }
}
