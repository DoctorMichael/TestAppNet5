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

        public async Task<Test> GetSingleTestAsync(int testId)
        {
            return await _testRepository.GetSingleTestAsync(testId);
        }

        public async Task<Question> GetSingleQuestionAsync(int questionId)
        {
            return await _testRepository.GetSingleQuestionAsync(questionId);
        }

        public async Task<string> AddNewTestAsync(Test test)
        {
            try
            {
                var res1 = await _testRepository.AddNewTestAsync(test);
                var res2 = await _testRepository.UnitOfWork.SaveChangesAsync();
                return res1.ToString() + "   Saved Changes: " + res2.ToString();
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        public async Task<string> UpdateTestAsync(Test test)
        {
            try
            {
                var _test = await _testRepository.GetSingleTestAsync(test.Id);

                if (_test == null)
                    return "Error: Test with ID: " + test.Id + " not found.";

                var res1 = _testRepository.Update(test);
                var res2 = await _testRepository.UnitOfWork.SaveChangesAsync();
                return res1.ToString() + "   Saved Changes: " + res2.ToString();
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        public async Task<string> RemoveTestAsync(int testId)
        {
            try
            {
                var test = await _testRepository.GetSingleTestAsync(testId);

                if (test == null)
                    return "Error: Test with ID: " + testId + " not found.";

                var res1 = _testRepository.Delete(test);
                var res2 = await _testRepository.UnitOfWork.SaveChangesAsync();
                return res1.ToString() + "   Saved Changes: " + res2.ToString();
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
    }
}
