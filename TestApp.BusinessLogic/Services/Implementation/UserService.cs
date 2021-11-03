using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
            return await _userRepository.GetAllUsersAsync(includeUserAnswers);
        }

        public async Task<IEnumerable<Test>> GetAllTestsAsync(bool includeQuestions)
        {
            return await _testRepository.GetAllTestsAsync(includeQuestions);
        }

        public async Task<Test> GetSingleTestByIdAsync(int testId)
        {
            return await _testRepository.GetSingleTestAsync(testId);
        }

        public async Task<Test> GetSingleTestByTestNameAsync(string testName)
        {
            return await _testRepository.GetSingleTestAsync(testName);
        }

        public async Task<Question> GetSingleQuestionAsync(int questionId)
        {
            return await _testRepository.GetSingleQuestionAsync(questionId);
        }

        public async Task<int> AddNewTestAsync(Test test)
        {
            var res = await _testRepository.AddNewTestAsync(test);
            await _testRepository.UnitOfWork.SaveChangesAsync();
            return res.Id;
        }

        public async Task UpdateTestAsync(Test test)
        {
            if (test == null)
                throw new Exception();

            await _testRepository.Update(test);
            await _testRepository.UnitOfWork.SaveChangesAsync();
        }

        public async Task RemoveTestAsync(int testId)
        {
            Test removeTest = await GetSingleTestByIdAsync(testId);

            if (removeTest == null)
                throw new Exception();

            await _testRepository.Delete(removeTest);
            await _testRepository.UnitOfWork.SaveChangesAsync();
        }
    }
}
