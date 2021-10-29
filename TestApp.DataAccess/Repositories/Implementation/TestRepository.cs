using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestApp.DataAccess.Context;
using TestApp.DataAccess.Repositories.Interfaces;
using TestApp.Domain.Models;

namespace TestApp.DataAccess.Repositories.Implementation
{
    public class TestRepository : BaseRepository<Test, TestAppContext>, ITestRepository, IBaseRepository<Test>
    {
        protected readonly DbSet<Test> _dbSetTest;
        public TestRepository(TestAppContext context) : base(context)
        {
            _dbSetTest = context.Set<Test>();
        }

        public async Task<IEnumerable<Test>> GetAllTestsAsync(bool includeQuestions)
        {

            if (includeQuestions)
                return await _dbSetTest.AsNoTracking()
                                       .Include(t => t.Questions)
                                       .ThenInclude(q => q.Answers)
                                       .ToListAsync();
            else
                return await _dbSetTest.AsNoTracking()
                                       .ToListAsync();
        }

        public async Task<Test> GetSingleTestAsync(int testId)
        {
            return await _dbSetTest//.AsNoTracking()
                                   .Include(t => t.Questions)
                                   .ThenInclude(q => q.Answers)
                                   .FirstOrDefaultAsync(t => t.Id == testId);
        }

        public async Task<Test> GetSingleTestAsync(string testName)
        {
            return await _dbSetTest//.AsNoTracking()
                                   .Include(t => t.Questions)
                                   .ThenInclude(q => q.Answers)
                                   .FirstOrDefaultAsync(t => t.TestName == testName);
        }

        public async Task<Question> GetSingleQuestionAsync(int questionId)
        {

            return await _context.Questions.Include(q => q.Answers)
                                           .FirstOrDefaultAsync(q => q.Id == questionId);
        }

        public async Task<Test> AddNewTestAsync(Test test)
        {
            var res = await _dbSetTest.AddAsync(test);
            return res.Entity;
        }
    }
}
