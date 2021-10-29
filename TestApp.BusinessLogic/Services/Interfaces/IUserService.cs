using System.Collections.Generic;
using System.Threading.Tasks;
using TestApp.Domain.Models;

namespace TestApp.BusinessLogic.Services.Interfaces
{
    public interface IUserService
    {
        Task<IEnumerable<Test>> GetAllTestsAsync(bool inclQuestions);
        Task<Test> GetSingleTestByIdAsync(int testId);
        Task<Test> GetSingleTestByTestNameAsync(string testName);
        Task<Question> GetSingleQuestionAsync(int questionId);


        //Task<User> AuthenticateUserAsync(string name, string password);
        //Task<User> RegisterUserAsync(string name, string password);



        // ============== Extra features for: user.IsController = true; ==================
        Task<IEnumerable<User>> GetAllUsersAsync(bool includeUserAnswers);
        Task<int> AddNewTestAsync(Test test);
        Task UpdateTestAsync(Test test);
        Task RemoveTestAsync(Test test);


        //Task AddQuestionAsync(Question question);
        //Task AddAnswerAsync(Answer answer);

        //Task RemoveQuestionAsync(int questionId);
        //Task RemoveAnswerAsync(int answerId);
    }
}
