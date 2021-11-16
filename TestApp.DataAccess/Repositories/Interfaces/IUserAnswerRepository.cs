using System.Collections.Generic;
using System.Threading.Tasks;
using TestApp.Domain.Models;

namespace TestApp.DataAccess.Repositories.Interfaces
{
    public interface IUserAnswerRepository : IBaseRepository<UserAnswer>
    {
        Task<IEnumerable<UserAnswer>> GetUserAnswersForTestAsync(int userId, int testId);
        Task<UserAnswer> AddNewUserAnswerAsync(UserAnswer userAnswer);
    }
}
