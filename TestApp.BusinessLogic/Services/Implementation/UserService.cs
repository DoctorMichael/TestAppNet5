using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestApp.BusinessLogic.Exceptions;
using TestApp.BusinessLogic.Services.Interfaces;
using TestApp.DataAccess.Context;
using TestApp.DataAccess.Repositories.Interfaces;
using TestApp.Domain.Models;

namespace TestApp.BusinessLogic.Services.Implementation
{
    public class UserService : IUserService
    {
        readonly IUserRepository _userRepository;
        readonly ITestRepository _testRepository;

        public UserService(IUserRepository userRepository, ITestRepository testRepository)
        {
            _userRepository = userRepository;
            _testRepository = testRepository;
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
            var res = await _testRepository.GetAllQuestionsAsync(includeAnswers);

            if (res == null || !res.Any())
                throw new ItemNotFoundException("Impossible to get any Questions.");

            return res;
        }

        public async Task<Test> GetSingleTestByIdAsync(int testId)
        {
            var res = await _testRepository.GetSingleTestAsync(testId);

            if (res == null)
                throw new ItemNotFoundException("Unable to get Test. Test ID: " + testId + " not found.");

            return res;
        }

        public async Task<bool> IsExistTestWithTestNameAsync(string testName)
        {
            var res = await _testRepository.GetSingleTestAsync(testName);

            if (res != null)
                return true;
            else
                return false;
        }

        public async Task<Question> GetSingleQuestionAsync(int questionId)
        {
            var res = await _testRepository.GetSingleQuestionAsync(questionId);

            if (res == null)
                throw new ItemNotFoundException("Unable to get Question. Question ID: " + questionId + " not found.");

            return res;
        }

        public async Task<int> AddNewTestAsync(Test test)
        {
            if (test == null)
                throw new NullReferenceException("Unable to add Test. Test not exist.");
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

            var res = await _testRepository.AddNewQuestionAsync(question);
            await _testRepository.UnitOfWork.SaveChangesAsync();

            return res.Id;
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

        public async Task<int> RemoveTestAsync(int testId)
        {
            Test removeTest = await _testRepository.GetSingleTestAsync(testId);

            if (removeTest == null)
                throw new ItemNotFoundException("Unable to remove. Test ID: " + testId + " not found.");

            await _testRepository.Delete(removeTest);
            await _testRepository.UnitOfWork.SaveChangesAsync();

            return testId;
        }
    }
}
