using System.Collections.Generic;
using System.Threading.Tasks;
using TestApp.Domain.Models;

namespace TestApp.DataAccess.Repositories.Interfaces
{
    public interface IUserRepository : IBaseRepository<User>
    {
        Task<IEnumerable<User>> GetAllUsersAsync(bool includeUserAnswers);
        Task<User> GetSingleUserAsync(int userId);
        Task<User> GetUserIdAsync(string name, string password);
        Task<User> AddNewUserAsync(User user);
    }
}
