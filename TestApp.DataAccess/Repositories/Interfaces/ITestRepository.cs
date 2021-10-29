using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestApp.Domain.Models;

namespace TestApp.DataAccess.Repositories.Interfaces
{
    public interface ITestRepository : IBaseRepository<Test>
    {
        Task<IEnumerable<Test>> GetAllTestsAsync(bool includeQuestions);
        Task<Test> GetSingleTestAsync(int testId);
        Task<Test> GetSingleTestAsync(string testName);
        Task<Question> GetSingleQuestionAsync(int questionId);
        Task<Test> AddNewTestAsync(Test test);
    }
}
