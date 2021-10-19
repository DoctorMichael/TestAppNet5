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
        Task<List<Test>> GetAllTestsAsync(bool inclQuestions);
        Task<Test> GetSingleTestAsync(int testId);
        Task<Question> GetSingleQuestionAsync(int questionId);


        //Task<User> AuthenticateUserAsync(string name, string password);
        //Task<User> RegisterUserAsync(string name, string password);



        //// ============== Extra features for: user.IsController = true; ==================
        Task<List<User>> GetAllUsersAsync(bool includeUserAnswers);
        Task AddNewTestAsync(Test test);
        Task UpdateTestAsync(Test test);
        Task RemoveTestAsync(int testId);


        //Task AddQuestionAsync(Question question);
        //Task AddAnswerAsync(Answer answer);

        //Task RemoveQuestionAsync(int questionId);
        //Task RemoveAnswerAsync(int answerId);
    }
}
