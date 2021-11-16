using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
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

        public UserController(IMapper mapper, IUserService userService)
        {
            _mapper = mapper;
            _userService = userService;
        }


        [HttpGet("GetAllUsers{includeUserAnswers}")]
        public OkObjectResult GetAllUsers(bool includeUserAnswers)
        {
            var res = _userService.GetAllUsersAsync(includeUserAnswers).Result;

            List<UserDto> userDtos = new();

            foreach (var item in res)
            {
                userDtos.Add(new UserDto(item));
            }

            return Ok(userDtos);
        }


        [HttpGet("GetAllTests{includeQuestions}")]
        public OkObjectResult GetAllTests(bool includeQuestions)
        {
            var res = _userService.GetAllTestsAsync(includeQuestions).Result;

            List<TestDto> testDtos = new();

            foreach (var item in res)
            {
                testDtos.Add(new TestDto(item));
            }

            return Ok(testDtos);
        }


        [HttpGet("GetAllQuestions{includeAnswers}")]
        public OkObjectResult GetAllQuestions(bool includeAnswers)
        {
            var res = _userService.GetAllQuestionsAsync(includeAnswers).Result;

            List<QuestionDto> questionDtos = new();

            foreach (var item in res)
            {
                questionDtos.Add(new QuestionDto(item));
            }

            return Ok(questionDtos);
        }



        [HttpPost("AddNewUser")]
        public ActionResult AddNewUser(AddUserDto addUserDto)
        {
            User user = new() { Name = addUserDto.Name, Password = addUserDto.Password, IsController = addUserDto.IsController };

            var addedUserId = _userService.AddNewUserAsync(user).Result;

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
        public ActionResult AddNewTest(string testName, int[] questionIds)
        {
            Test test = new() { TestName = testName };

            if (questionIds != null)
            {
                test.Questions = new List<Question>();

                foreach (var id in questionIds)
                {
                    var question = _userService.GetSingleQuestionAsync(id);

                    if (question?.Result != null)
                        test.Questions.Add(question.Result);
                }
            }

            var addedTestId = _userService.AddNewTestAsync(test).Result;

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
        public ActionResult AddNewQuestion(string questionText, AddAnswerDto[] answers)
        {
            Question question = new() { QuestionText = questionText };

            if (answers != null)
            {
                question.Answers = new List<Answer>();

                foreach (var item in answers)
                {
                    question.Answers.Add(new Answer { AnswerText = item.AnswerText, IsCorrect = item.IsCorrect });
                }
            }

            var addedQuestionId = _userService.AddNewQuestionAsync(question).Result;

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
        public ActionResult AddNewUserAnswer(UserAnswerDto userAnswerDto)
        {
            UserAnswer userAnswer = new() { UserID = userAnswerDto.UserID, TestID = userAnswerDto.TestID, AnswerID = userAnswerDto.AnswerID };

            var userAnswerReturned = _userService.AddNewUserAnswerAsync(userAnswer).Result;

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
        public OkObjectResult RemoveUserById(int userId)
        {
            var res = _userService.RemoveUserAsync(userId);

            return Ok("User ID: " + res.Result + " removed successfully.");
        }


        [HttpDelete("RemoveTest{testId}")]
        public OkObjectResult RemoveTestById(int testId)
        {
            var res = _userService.RemoveTestAsync(testId);

            return Ok("Test ID: " + res.Result + " removed successfully.");
        }


        [HttpDelete("RemoveQuestion{questionId}")]
        public OkObjectResult RemoveQuestionById(int questionId)
        {
            var res = _userService.RemoveQuestionAsync(questionId);

            return Ok("Question ID: " + res.Result + " removed successfully.");
        }


        [HttpDelete("RemoveUserAnswers")]
        public ActionResult RemoveUserAnswers(int userId, int testId)
        {
            var res = _userService.RemoveUserAnswersForTestAsync(userId, testId);

            if (res.Result > 0)
            {
                return Ok(res.Result + " UserAnswers for User ID: " + userId + " and Test ID: " + testId + " removed successfully.");
            }
            else
            {
                return StatusCode(400, "Failed to remove UserAnswers.");
            }
        }



        [HttpPatch("AddQuestionsToTest")]
        public OkObjectResult AddQuestionsToTest(int testId, int[] questionIds)
        {
            Test test = _userService.GetSingleTestByIdAsync(testId).Result;

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
                        Question question = _userService.GetSingleQuestionAsync(id).Result;

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
                    Question question = _userService.GetSingleQuestionAsync(id).Result;

                    if (question != null)
                        test.Questions.Add(question);
                }
            }

            var updatedTest = _userService.UpdateTestAsync(test).Result;

            return Ok(new TestDto(updatedTest));
        }


        [HttpPatch("RemoveQuestionsFromTest")]
        public OkObjectResult RemoveQuestionsFromTest(int testId, int[] questionIds)
        {
            Test test = _userService.GetSingleTestByIdAsync(testId).Result;

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
                        Question question = _userService.GetSingleQuestionAsync(id).Result;

                        if (question != null)
                            test.Questions.Remove(question);
                    }
                }
            }

            var updatedTest = _userService.UpdateTestAsync(test).Result;

            return Ok(new TestDto(updatedTest));
        }
    }
}
