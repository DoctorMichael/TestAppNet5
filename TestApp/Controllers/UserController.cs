using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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



        [HttpGet]
        public OkObjectResult GetAllTest()
        {
            var res = _userService.GetAllTestsAsync(true).Result;

            List<TestDto> testDtos = new();

            foreach (var item in res)
            {
                testDtos.Add(new TestDto(item));
            }

            return Ok(testDtos);
        }


        [HttpPost("{name}")]
        public OkObjectResult AddNewTest(string name)
        {
            Test test = new() { TestName = name, Questions = new List<Question>() { new() { QuestionText = "Quest1" }, new() { QuestionText = "Quest2" } } };

            var res = _userService.AddNewTestAsync(test);

            return Ok(res.Result);
        }


        [HttpDelete("{testId}")]
        public OkObjectResult RemoveTestById(int testId)
        {
            var res = _userService.RemoveTestAsync(testId);

            return Ok(res);
        }


        [HttpPatch("{testId}")]
        public OkObjectResult UpdateTestById(int testId)
        {
            Test test = _userService.GetSingleTestByIdAsync(testId).Result;

            if (test?.Questions?.Last() != null)
            {
                //test.TestName = "U_" + test.TestName; // Change TestName for exception.
                test.Questions.Last().QuestionText = "U_" + test.Questions.Last().QuestionText;
            }

            var res = _userService.UpdateTestAsync(test);

            return Ok(res);
        }
    }
}
