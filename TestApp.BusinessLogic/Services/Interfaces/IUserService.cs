using System.Collections.Generic;
using System.Threading.Tasks;
using TestApp.Domain.Models;

namespace TestApp.BusinessLogic.Services.Interfaces
{
    public interface IUserService
    {
        Task<IEnumerable<Test>> GetAllTestsAsync(bool inclQuestions);
        Task<Test> GetSingleTestByIdAsync(int testId);
        Task<Question> GetSingleQuestionAsync(int questionId);
        Task<IEnumerable<UserAnswer>> GetUserAnswersForTestAsync(int userId, int testId);


        //Task<User> AuthenticateUserAsync(string name, string password);
        //Task<User> RegisterUserAsync(string name, string password);


        // ============== Extra features for: user.IsController = true; ==================
        Task<IEnumerable<User>> GetAllUsersAsync(bool includeUserAnswers);
        Task<IEnumerable<Question>> GetAllQuestionsAsync(bool includeAnswers);
        Task<User> GetSingleUserByIdAsync(int userId);

        Task<int> AddNewUserAsync(User user);
        Task<int> AddNewTestAsync(string testName, List<int> questionIds);
        Task<int> AddNewQuestionAsync(Question question);
        Task<UserAnswer> AddNewUserAnswerAsync(UserAnswer userAnswer);
        Task<Test> AddQuestionsToTestAsync(int testId, List<int> questionIds);

        Task<int> RemoveUserAsync(int userId);
        Task<int> RemoveTestAsync(int testId);
        Task<int> RemoveQuestionAsync(int questionId);
        Task<int> RemoveUserAnswersForTestAsync(int userId, int testId);
        Task<Test> RemoveQuestionsFromTestAsync(int testId, List<int> questionIds);

        Task<Test> UpdateTestAsync(Test test);

        Task<bool> IsExistTestWithTestNameAsync(string testName);
    }
}
