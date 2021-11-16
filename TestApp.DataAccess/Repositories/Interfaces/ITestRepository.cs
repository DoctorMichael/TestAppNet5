using System.Collections.Generic;
using System.Threading.Tasks;
using TestApp.Domain.Models;

namespace TestApp.DataAccess.Repositories.Interfaces
{
    public interface ITestRepository : IBaseRepository<Test>
    {
        Task<IEnumerable<Test>> GetAllTestsAsync(bool includeQuestions);
        Task<Test> GetSingleTestAsync(int testId);
        Task<Test> GetSingleTestAsync(string testName);
        Task<Test> AddNewTestAsync(Test test);
    }
}
