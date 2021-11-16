using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestApp.BusinessLogic.Exceptions;
using TestApp.BusinessLogic.Services.Interfaces;
using TestApp.DataAccess.Repositories.Interfaces;
using TestApp.Domain.Models;

namespace TestApp.BusinessLogic.Services.Implementation
{
    public class UserService : IUserService
    {
        readonly IUserRepository _userRepository;
        readonly ITestRepository _testRepository;
        readonly IQuestionRepository _questionRepository;
        readonly IUserAnswerRepository _userAnswerRepository;

        public UserService(IUserRepository userRepository, ITestRepository testRepository, 
                           IQuestionRepository questionRepository, IUserAnswerRepository userAnswerRepository)
        {
            _userRepository = userRepository;
            _testRepository = testRepository;
            _questionRepository = questionRepository;
            _userAnswerRepository = userAnswerRepository;
        }


        public async Task<IEnumerable<User>> GetAllUsersAsync(bool includeUserAnswers)
        {
            var res = await _userRepository.GetAllUsersAsync(includeUserAnswers);

            if (res == null || !res.Any())
                throw new ItemNotFoundException("Impossible to get any Users.");

            return res;
        }

        public async Task<IEnumerable<Test>> GetAllTestsAsync(bool includeQuestions)
        {
            var res = await _testRepository.GetAllTestsAsync(includeQuestions);

            if (res == null || !res.Any())
                throw new ItemNotFoundException("Impossible to get any Tests.");

            return res;
        }

        public async Task<IEnumerable<Question>> GetAllQuestionsAsync(bool includeAnswers)
        {
            var res = await _questionRepository.GetAllQuestionsAsync(includeAnswers);

            if (res == null || !res.Any())
                throw new ItemNotFoundException("Impossible to get any Questions.");

            return res;
        }

        public async Task<IEnumerable<UserAnswer>> GetUserAnswersForTestAsync(int userId, int testId)
        {
            var res = await _userAnswerRepository.GetUserAnswersForTestAsync(userId, testId);

            if (res == null || !res.Any())
                throw new ItemNotFoundException("Impossible to get any UserAnswer for UserId : " + userId + " TestId: " + testId);

            return res;
        }


        public async Task<User> GetSingleUserByIdAsync(int userId)
        {
            var res = await _userRepository.GetSingleUserAsync(userId);

            if (res == null)
                throw new ItemNotFoundException("Unable to get User. User ID: " + userId + " not found.");

            return res;
        }

        public async Task<Test> GetSingleTestByIdAsync(int testId)
        {
            var res = await _testRepository.GetSingleTestAsync(testId);

            if (res == null)
                throw new ItemNotFoundException("Unable to get Test. Test ID: " + testId + " not found.");

            return res;
        }

        public async Task<Question> GetSingleQuestionAsync(int questionId)
        {
            var res = await _questionRepository.GetSingleQuestionAsync(questionId);

            if (res == null)
                throw new ItemNotFoundException("Unable to get Question. Question ID: " + questionId + " not found.");

            return res;
        }


        public async Task<int> AddNewUserAsync(User user)
        {
            if (user == null)
                throw new NullReferenceException("Unable to add User. New User not specified.");

            var res = await _userRepository.AddNewUserAsync(user);
            await _userRepository.UnitOfWork.SaveChangesAsync();

            return res.Id;
        }

        public async Task<UserAnswer> AddNewUserAnswerAsync(UserAnswer userAnswer)
        {
            if (userAnswer == null)
                throw new NullReferenceException("Unable to add UserAnswer. New UserAnswer not specified.");

            var res = await _userAnswerRepository.AddNewUserAnswerAsync(userAnswer);
            await _userAnswerRepository.UnitOfWork.SaveChangesAsync();

            return res;
        }

        public async Task<int> AddNewTestAsync(Test test)
        {
            if (test == null)
                throw new NullReferenceException("Unable to add Test. New Test not specified.");
            else if (test.TestName == null || test.TestName == "")
                throw new IncorrectDataException("Unable to add Test. TestName not specified.");
            else if (IsExistTestWithTestNameAsync(test.TestName).Result)
                throw new ItemAlreadyExistException("Unable to add Test. TestName: " + test.TestName + " allready exist.");

            var res = await _testRepository.AddNewTestAsync(test);
            await _testRepository.UnitOfWork.SaveChangesAsync();

            return res.Id;
        }

        public async Task<int> AddNewQuestionAsync(Question question)
        {
            if (question == null)
                throw new NullReferenceException("Unable to add Question. Question not exist.");

            var res = await _questionRepository.AddNewQuestionAsync(question);
            await _questionRepository.UnitOfWork.SaveChangesAsync();

            return res.Id;
        }


        public async Task<int> RemoveUserAsync(int userId)
        {
            User removeUser = await _userRepository.GetSingleUserAsync(userId);

            if (removeUser == null)
                throw new ItemNotFoundException("Unable to remove. User ID: " + userId + " not found.");

            await _userRepository.Delete(removeUser);
            await _userRepository.UnitOfWork.SaveChangesAsync();

            return userId;
        }

        public async Task<int> RemoveTestAsync(int testId)
        {
            Test removeTest = await _testRepository.GetSingleTestAsync(testId);

            if (removeTest == null)
                throw new ItemNotFoundException("Unable to remove. Test ID: " + testId + " not found.");

            await _testRepository.Delete(removeTest);
            await _testRepository.UnitOfWork.SaveChangesAsync();

            return testId;
        }

        public async Task<int> RemoveQuestionAsync(int questionId)
        {
            Question removeQuestion = await _questionRepository.GetSingleQuestionAsync(questionId);

            if (removeQuestion == null)
                throw new ItemNotFoundException("Unable to remove. Question ID: " + questionId + " not found.");

            await _questionRepository.Delete(removeQuestion);
            await _questionRepository.UnitOfWork.SaveChangesAsync();

            return questionId;
        }

        public async Task<int> RemoveUserAnswersForTestAsync(int userId, int testId)
        {
            var user = await _userRepository.GetSingleUserAsync(userId);

            if (user == null)
                throw new ItemNotFoundException("Unable to remove. UserId : " + userId + " not found.");

            if (user.UserAnswers == null || !user.UserAnswers.Any())
                throw new ItemNotFoundException("Unable to remove. UserAnswers for UserId : " + userId + " not found.");

            var userAnswers = user.UserAnswers.Where(u => u.TestID == testId);

            if (userAnswers == null || !userAnswers.Any())
                throw new ItemNotFoundException("Unable to remove. UserAnswers for UserId : " + userId + " and TestId: " + testId + " not found.");

            int userAnswersCount = userAnswers.ToArray().Length;

            var res = await _userAnswerRepository.DeleteRange(userAnswers.ToArray());
            await _userAnswerRepository.UnitOfWork.SaveChangesAsync();

            return res;
        }


        public async Task<Test> UpdateTestAsync(Test test)
        {
            if (test == null)
                throw new NullReferenceException("Unable to update Test. Test not exist.");
            else if (test.Id <= 0)
                throw new IncorrectDataException("Unable to update Test. Test Id must be a positive number.");
            else if (test.TestName == null || test.TestName == "")
                throw new IncorrectDataException("Unable to update Test. TestName not specified.");

            await _testRepository.Update(test);
            await _testRepository.UnitOfWork.SaveChangesAsync();

            return await _testRepository.GetSingleTestAsync(test.Id);
        }


        public async Task<bool> IsExistTestWithTestNameAsync(string testName)
        {
            var res = await _testRepository.GetSingleTestAsync(testName);

            return res != null;
        }

    }
}
