using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestApp.BusinessLogic.Exceptions;
using TestApp.BusinessLogic.Services.Implementation;
using TestApp.BusinessLogic.Services.Interfaces;
using TestApp.DataAccess.Context;
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



        [HttpGet("GetAllTests{includeQuestions}")]
        public OkObjectResult GetAllTest(bool includeQuestions)
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


        [HttpPost("AddNewTest")]
        public OkObjectResult AddNewTest(string testName, int[] questionIds)
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
                return Ok(res);
            }
        }


        [HttpPost("AddNewQuestion")]
        public OkObjectResult AddNewQuestion(string questionText, AnswerLightDto[] answers)
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
                return Ok(res);
            }
        }


        [HttpDelete("RemoveTest{testId}")]
        public OkObjectResult RemoveTestById(int testId)
        {
            var res = _userService.RemoveTestAsync(testId);

            return Ok("Test ID: " + res.Result + " removed successfully.");
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
