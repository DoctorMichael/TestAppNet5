using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
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


        [HttpGet("GetAllUsers/{includeUserAnswers}")]
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

        [HttpGet("GetUserId")]
        public async Task<ActionResult> GetUserId(string name, string password)
        {
            var res = await _userService.GetUserIdAsync(name, password);

            return Ok(res);
        }


        [HttpGet("GetAllTests/{includeQuestions}")]
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


        [HttpGet("GetAllQuestions/{includeAnswers}")]
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


        [HttpGet("GetSingleTest/{testId}")]
        public async Task<ActionResult> GetSingleTest(int testId)
        {
            var res = await _userService.GetSingleTestByIdAsync(testId);

            return Ok(new TestDto(res));
        }


        [HttpGet("GetUserAnswersForTest")]
        public async Task<ActionResult> GetUserAnswersForTest(int userId, int testId)
        {
            var res = await _userService.GetUserAnswersForTestAsync(userId, testId);

            List<UserAnswerDto> userAnswers = new();

            foreach (var item in res)
            {
                userAnswers.Add(new UserAnswerDto(item));
            }

            return Ok(userAnswers);
        }


        [HttpGet("CheckCorrectnessUserAnswersForTest")]
        public async Task<ActionResult> CheckCorrectnessUserAnswersForTest(int userId, int testId)
        {
            Test test = await _userService.CheckCorrectnessUserAnswersForTestAsync(userId, testId);

            return Ok(new CheckUserAnswersForTestDto(test.Questions) { UserId = userId, TestId = testId, TestName = test.TestName});
        }


        [HttpPost("AddNewUser")]
        public async Task<ActionResult> AddNewUser(CreateUserDto addUserDto)
        {
            if (!ModelState.IsValid) { return BadRequest(ModelState); }

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
        public async Task<ActionResult> AddNewTest(CreateTestDto newTest)
        {
            if (!ModelState.IsValid) { return BadRequest(ModelState); }

            var addedTestId = await _userService.AddNewTestAsync(newTest.TestName, newTest.QuestionIds);

            (string message, int newTestId) res = ("New Test ID: " + addedTestId + ", Test Name: " + newTest.TestName + " added successfully.", addedTestId);

            if (addedTestId > 0)
            {
                return Ok(res);
            }
            else
            {
                res = ("Failed to add new Test: " + newTest.TestName, addedTestId);
                return StatusCode(400, res);
            }
        }


        [HttpPost("AddNewQuestion")]
        public async Task<ActionResult> AddNewQuestion(CreateQuestionDto createQuestionDto)
        {
            if (!ModelState.IsValid) { return BadRequest(ModelState); }

            Question question = new() { QuestionText = createQuestionDto.QuestionText, Answers = new List<Answer>() };

            foreach (var item in createQuestionDto.CreateAnswerDtos)
            {
                question.Answers.Add(new Answer { AnswerText = item.AnswerText, IsCorrect = item.IsCorrect });
            }

            var addedQuestionId = await _userService.AddNewQuestionAsync(question);

            (string message, int newQuestionId) res = ("New Question ID: " + addedQuestionId + ", Question Text: " +
                                                       createQuestionDto.QuestionText + " added successfully.", addedQuestionId);

            if (addedQuestionId > 0)
            {
                return Ok(res);
            }
            else
            {
                res = ("Failed to add new Question: " + createQuestionDto.QuestionText, addedQuestionId);
                return StatusCode(400, res);
            }
        }


        [HttpPost("AddNewUserAnswer")]
        public async Task<ActionResult> AddNewUserAnswer(UserAnswerDto userAnswerDto)
        {
            if (!ModelState.IsValid) { return BadRequest(ModelState); }

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



        [HttpDelete("RemoveUser/{userId}")]
        public async Task<ActionResult> RemoveUserById(int userId)
        {
            var res = await _userService.RemoveUserAsync(userId);

            return Ok("User ID: " + res + " removed successfully.");
        }


        [HttpDelete("RemoveTest/{testId}")]
        public async Task<ActionResult> RemoveTestById(int testId)
        {
            var res = await _userService.RemoveTestAsync(testId);

            return Ok("Test ID: " + res + " removed successfully.");
        }


        [HttpDelete("RemoveQuestion/{questionId}")]
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
        public async Task<ActionResult> AddQuestionsToTest(TestWithQuestionsDto testAndQuestionsDto)
        {
            if (!ModelState.IsValid) { return BadRequest(ModelState); }

            Test updatedTest = await _userService.AddQuestionsToTestAsync(testAndQuestionsDto.TestId, testAndQuestionsDto.QuestionIds);

            return Ok(new TestDto(updatedTest));
        }


        [HttpPatch("RemoveQuestionsFromTest")]
        public async Task<ActionResult> RemoveQuestionsFromTest(TestWithQuestionsDto testAndQuestionsDto)
        {
            if (!ModelState.IsValid) { return BadRequest(ModelState); }

            Test updatedTest = await _userService.RemoveQuestionsFromTestAsync(testAndQuestionsDto.TestId, testAndQuestionsDto.QuestionIds);

            return Ok(new TestDto(updatedTest));
        }
    }
}
