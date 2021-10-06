using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestApp.Domain.Models;

namespace TestApp.BusinessLogic.Services.Interfaces
{
    public interface IUserService
    {
        Task<User> AuthenticateUserAsync(string name, string password);
        Task<User> RegisterUserAsync(string name, string password);


        Task<List<Test>> GetTestListAsync();
        Task<Test> GetQuestionListAsync(Test test);
        Task<Answer> GetAnswerAsync(Question question);
        Task<User> GetUserAnswerSetAsync(User user);


        // ============== Extra features for: user.IsController = true; ==================
        Task<List<User>> GetUserListAsync(User user);

        Task<Question> AddQuestionAsync(string questionText);
        Task<Answer> AddAnswerAsync(Answer answer);

        Task<bool> RemoveQuestionAsync(Question question);
        Task<bool> RemoveAnswerAsync(Answer answer);
    }
}
