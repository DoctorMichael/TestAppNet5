using System.Collections.Generic;
using System.Threading.Tasks;
using TestApp.Domain.Models;

namespace TestApp.BusinessLogic.Services.Interfaces
{
    public interface IUserService
    {
        Task<IEnumerable<Test>> GetAllTestsAsync(bool inclQuestions);
        Task<Test> GetSingleTestAsync(int testId);
        Task<Question> GetSingleQuestionAsync(int questionId);


        //Task<User> AuthenticateUserAsync(string name, string password);
        //Task<User> RegisterUserAsync(string name, string password);



        // ============== Extra features for: user.IsController = true; ==================
        Task<IEnumerable<User>> GetAllUsersAsync(bool includeUserAnswers);
        Task<string> AddNewTestAsync(Test test);
        Task<string> UpdateTestAsync(Test test);
        Task<string> RemoveTestAsync(int testId);


        //Task AddQuestionAsync(Question question);
        //Task AddAnswerAsync(Answer answer);

        //Task RemoveQuestionAsync(int questionId);
        //Task RemoveAnswerAsync(int answerId);
    }
}
