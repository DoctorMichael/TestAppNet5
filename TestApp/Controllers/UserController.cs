using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using TestApp.BusinessLogic.Exceptions;
using TestApp.BusinessLogic.Services.Interfaces;
using TestApp.Domain.Models;
using TestApp.DTOs;

namespace TestApp.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        readonly IMapper _mapper;
        readonly IUserService _userService;
        readonly IValidator<CreateAnswerDto> _validatorAnswerDto;
        readonly IValidator<CreateUserDto> _validatorUserDto;
        readonly IValidator<object> _validatorNotNull;

        public UserController(IMapper mapper, IUserService userService, IValidator<CreateAnswerDto> validatorAnswerDto,
                              IValidator<CreateUserDto> validatorUserDto, IValidator<object> validatorNotNull)
        {
            _mapper = mapper;
            _userService = userService;
            _validatorAnswerDto = validatorAnswerDto;
            _validatorUserDto = validatorUserDto;
            _validatorNotNull = validatorNotNull;
        }


        [HttpGet("GetAllUsers{includeUserAnswers}")]
        public async Task<ActionResult> GetAllUsers(bool includeUserAnswers)
        {
            var res = await _userService.GetAllUsersAsync(includeUserAnswers);

            List<UserDto> userDtos = new();

            foreach (var item in res)
            {
                userDtos.Add(new UserDto(item));
            }

            return Ok(userDtos);
        }


        [HttpGet("GetAllTests{includeQuestions}")]
        public async Task<ActionResult> GetAllTests(bool includeQuestions)
        {
            var res = await _userService.GetAllTestsAsync(includeQuestions);

            List<TestDto> testDtos = new();

            foreach (var item in res)
            {
                testDtos.Add(new TestDto(item));
            }

            return Ok(testDtos);
        }


        [HttpGet("GetAllQuestions{includeAnswers}")]
        public async Task<ActionResult> GetAllQuestions(bool includeAnswers)
        {
            var res = await _userService.GetAllQuestionsAsync(includeAnswers);

            List<QuestionDto> questionDtos = new();

            foreach (var item in res)
            {
                questionDtos.Add(new QuestionDto(item));
            }

            return Ok(questionDtos);
        }



        [HttpPost("AddNewUser")]
        public async Task<ActionResult> AddNewUser(CreateUserDto addUserDto)
        {
            _validatorUserDto.ValidateAndThrow(addUserDto);

            User user = new() { Name = addUserDto.Name, Password = addUserDto.Password, IsController = addUserDto.IsController };

            var addedUserId = await _userService.AddNewUserAsync(user);

            (string message, int newUserId) res = ("New User ID: " + addedUserId + ", User Name: " + user.Name + " added successfully.", addedUserId);

            if (addedUserId > 0)
            {
                return Ok(res);
            }
            else
            {
                res = ("Failed to add new User: " + user.Name, addedUserId);
                return StatusCode(400, res);
            }
        }


        [HttpPost("AddNewTest")]
        public async Task<ActionResult> AddNewTest(string testName, int[] questionIds)
        {
            Test test = new() { TestName = testName };

            if (questionIds != null)
            {
                test.Questions = new List<Question>();

                foreach (var id in questionIds)
                {
                    var question = await _userService.GetSingleQuestionAsync(id);

                    if (question != null)
                        test.Questions.Add(question);
                }
            }

            var addedTestId = await _userService.AddNewTestAsync(test);

            (string message, int newTestId) res = ("New Test ID: " + addedTestId + ", Test Name: " + testName + " added successfully.", addedTestId);

            if (addedTestId > 0)
            {
                return Ok(res);
            }
            else
            {
                res = ("Failed to add new Test: " + test.TestName, addedTestId);
                return StatusCode(400, res);
            }
        }


        [HttpPost("AddNewQuestion")]
        public async Task<ActionResult> AddNewQuestion(string questionText, CreateAnswerDto[] answers)
        {
            Question question = new() { QuestionText = questionText };

            if (answers != null)
            {
                question.Answers = new List<Answer>();

                foreach (var item in answers)
                {
                    _validatorAnswerDto.ValidateAndThrow(item);
                    question.Answers.Add(new Answer { AnswerText = item.AnswerText, IsCorrect = item.IsCorrect });
                }
            }

            var addedQuestionId = await _userService.AddNewQuestionAsync(question);

            (string message, int newQuestionId) res = ("New Question ID: " + addedQuestionId + ", Question Text: " + questionText + " added successfully.", addedQuestionId);

            if (addedQuestionId > 0)
            {
                return Ok(res);
            }
            else
            {
                res = ("Failed to add new Question: " + questionText, addedQuestionId);
                return StatusCode(400, res);
            }
        }


        [HttpPost("AddNewUserAnswer")]
        public async Task<ActionResult> AddNewUserAnswer(UserAnswerDto userAnswerDto)
        {
            UserAnswer userAnswer = new() { UserID = userAnswerDto.UserID, TestID = userAnswerDto.TestID, AnswerID = userAnswerDto.AnswerID };

            var userAnswerReturned = await _userService.AddNewUserAnswerAsync(userAnswer);

            if (userAnswerReturned != null)
            {
                return Ok("New UserAnswer added successfully.");
            }
            else
            {
                return StatusCode(400, "Failed to add new UserAnswer.");
            }
        }



        [HttpDelete("RemoveUser{userId}")]
        public async Task<ActionResult> RemoveUserById(int userId)
        {
            var res = await _userService.RemoveUserAsync(userId);

            return Ok("User ID: " + res + " removed successfully.");
        }


        [HttpDelete("RemoveTest{testId}")]
        public async Task<ActionResult> RemoveTestById(int testId)
        {
            var res = await _userService.RemoveTestAsync(testId);

            return Ok("Test ID: " + res + " removed successfully.");
        }


        [HttpDelete("RemoveQuestion{questionId}")]
        public async Task<ActionResult> RemoveQuestionById(int questionId)
        {
            var res = await _userService.RemoveQuestionAsync(questionId);

            return Ok("Question ID: " + res + " removed successfully.");
        }


        [HttpDelete("RemoveUserAnswers")]
        public async Task<ActionResult> RemoveUserAnswers(int userId, int testId)
        {
            var res = await _userService.RemoveUserAnswersForTestAsync(userId, testId);

            if (res > 0)
            {
                return Ok(res + " UserAnswers for User ID: " + userId + " and Test ID: " + testId + " removed successfully.");
            }
            else
            {
                return StatusCode(400, "Failed to remove UserAnswers.");
            }
        }



        [HttpPatch("AddQuestionsToTest")]
        public async Task<ActionResult> AddQuestionsToTest(int testId, int[] questionIds)
        {
            Test test = await _userService.GetSingleTestByIdAsync(testId);

            if (test == null || test.Id <= 0)
                throw new ItemNotFoundException("Unable to update. Test ID: " + testId + " not found.");


            if (test.Questions != null && questionIds != null)
            {
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
                        Question question = await _userService.GetSingleQuestionAsync(id);

                        if (question != null)
                            test.Questions.Add(question);
                    }
                }
            }
            else if (questionIds != null)
            {
                test.Questions = new List<Question>();

                foreach (var id in questionIds)
                {
                    Question question = await _userService.GetSingleQuestionAsync(id);

                    if (question != null)
                        test.Questions.Add(question);
                }
            }

            var updatedTest = await _userService.UpdateTestAsync(test);

            return Ok(new TestDto(updatedTest));
        }


        [HttpPatch("RemoveQuestionsFromTest")]
        public async Task<ActionResult> RemoveQuestionsFromTest(int testId, int[] questionIds)
        {
            Test test = await _userService.GetSingleTestByIdAsync(testId);

            if (test == null || test.Id <= 0)
                throw new ItemNotFoundException("Unable to update. Test ID: " + testId + " not found.");


            if (test.Questions != null && questionIds != null)
            {
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
                        Question question = await _userService.GetSingleQuestionAsync(id);

                        if (question != null)
                            test.Questions.Remove(question);
                    }
                }
            }

            Test updatedTest = await _userService.UpdateTestAsync(test);

            return Ok(new TestDto(updatedTest));
        }
    }
}
