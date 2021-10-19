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


            //var res2 = _mapper.Map<List<TestDto>>(res);
            //return Ok(res2);


            List<DTO<Question>> listDTO = new();

            foreach (var item in res)
            {
                List<Question> temp = new();

                foreach (var item2 in item.Questions)
                {
                    List<Answer> tempAnswer = new();

                    foreach (var item3 in item2.Answers)
                    {
                        tempAnswer.Add(new Answer { Id = item3.Id, AnswerText = item3.AnswerText, IsCorrect = item3.IsCorrect, QuestionId = item3.QuestionId });
                    }

                    temp.Add(new Question { Id = item2.Id, QuestionText = item2.QuestionText, Answers = tempAnswer });
                }

                listDTO.Add(new DTO<Question> { id = item.Id, name = item.TestName, text = item.ToString(), tl = temp });
            }

            //var shuffleListDTO = listLDTO.OrderBy(a => Guid.NewGuid()).ToList();

            return Ok(listDTO);

        }


        [HttpPost("{name}")]
        public OkResult AddNewTest(string name)
        {
            Test test = new() { TestName = name, Questions = new List<Question>() { new() { QuestionText = "Quest1" }, new() { QuestionText = "Quest2" } } };

            _userService.AddNewTestAsync(test);

            return Ok();
        }


        [HttpDelete("{testId}")]
        public OkResult RemoveTestById(int testId)
        {
            _userService.RemoveTestAsync(testId);

            return Ok();
        }
    }

    public class DTO<T> // Just for testing API response without circular references.
    {
        public int id { get; set; } = 0;
        public string text { get; set; } = "";
        public string name { get; set; } = "";
        public ICollection<T> tl { get; set; }
    }
}
