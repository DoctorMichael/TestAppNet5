using FluentValidation;
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
                throw new ItemNotFoundException("Impossible to get any UserAnswers for UserId: " + userId + " TestId: " + testId);

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

        public async Task<Test> CheckCorrectnessUserAnswersForTestAsync(int userId, int testId)
        {
            var userAnswers = await GetUserAnswersForTestAsync(userId, testId);
            var test = await GetSingleTestByIdAsync(testId);

            if (test.Questions == null || test.Questions.Count == 0)
                throw new ItemNotFoundException("Unable to get any Questions for Test ID: " + testId + ".");
            
            if (test.Questions.Count < userAnswers.Count())
                throw new ItemNotFoundException("Incorrect passing of the Test ID: " + testId + ". Number of Answers more than Questions.");


            Test resTest = new() { Id = test.Id, TestName = test.TestName, Questions = new List<Question>() };

            foreach (var item in test.Questions)
            {
                if (item.Answers == null || item.Answers.Count == 0)
                    throw new ItemNotFoundException("Invalid Test ID: " + testId + ". Question ID: " + item.Id + " has no answer options.");

                resTest.Questions.Add(new() { Id = item.Id, QuestionText = item.QuestionText, Answers = new List<Answer>() });

                bool userAnswerNotFound = true;

                foreach (var answer in item.Answers)
                {
                    if (userAnswers.FirstOrDefault(x => x.AnswerID == answer.Id) != null)
                    {
                        resTest.Questions.Last().Answers.Add(answer);
                        userAnswerNotFound = false;
                        break;
                    }
                }

                if (userAnswerNotFound)
                    resTest.Questions.Last().Answers.Add(new() { Id = -1, AnswerText = "Answer Not Found.", IsCorrect = false });
            }

            return resTest;
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

        public async Task<int> AddNewTestAsync(string testName, List<int> questionIds)
        {
            if (IsExistTestWithTestNameAsync(testName).Result)
                throw new ItemAlreadyExistException("Unable to add Test. TestName: " + testName + " allready exist.");

            Test test = new() { TestName = testName, Questions = new List<Question>() };

            foreach (var id in questionIds)
            {
                var question = await GetSingleQuestionAsync(id);

                if (question != null)
                    test.Questions.Add(question);
                else
                    throw new ItemNotFoundException("Unable to add Test. Question ID: " + id + " not found.");
            }

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

        public async Task<Test> AddQuestionsToTestAsync(int testId, List<int> questionIds)
        {
            Test test = await GetSingleTestByIdAsync(testId);

            if (test == null || test.Id <= 0)
                throw new ItemNotFoundException("Unable to update. Test ID: " + testId + " not found.");

            if (test.Questions == null)
                test.Questions = new List<Question>();


            foreach (var id in questionIds)
            {
                bool needAddQuestion = true;

                foreach (var item2 in test.Questions)
                {
                    if (id == item2.Id)
                    {
                        needAddQuestion = false;
                        break;
                    }
                }

                if (needAddQuestion)
                {
                    Question question = await GetSingleQuestionAsync(id);

                    if (question != null)
                        test.Questions.Add(question);
                    else
                        throw new ItemNotFoundException("Unable to update Test. Questions ID: " + id + " not found.");
                }
            }

            return await UpdateTestAsync(test);
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

        public async Task<Test> RemoveQuestionsFromTestAsync(int testId, List<int> questionIds)
        {
            Test test = await GetSingleTestByIdAsync(testId);

            if (test == null || test.Id <= 0)
                throw new ItemNotFoundException("Unable to update. Test ID: " + testId + " not found.");

            if (test.Questions == null)
                throw new ItemNotFoundException("Unable to update. Test ID: " + testId + " doesn't contain any questions.");


            foreach (var id in questionIds)
            {
                bool needRemoveQuestion = false;

                foreach (var item2 in test.Questions)
                {
                    if (id == item2.Id)
                    {
                        needRemoveQuestion = true;
                        break;
                    }
                }

                if (needRemoveQuestion)
                {
                    Question question = await GetSingleQuestionAsync(id);

                    if (question != null)
                        test.Questions.Remove(question);
                }
            }

            return await UpdateTestAsync(test);
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
