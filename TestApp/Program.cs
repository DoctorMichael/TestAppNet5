using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestApp.BusinessLogic.Services.Implementation;
using TestApp.Domain.Models;
using TestApp.DataAccess.Context;
using Microsoft.EntityFrameworkCore;
using TestApp.DataAccess.InitializationDb;

namespace TestApp
{
    public class Program
    {
        public static void Main(string[] args)
        {
            //InitializationDb iDb = new InitializationDb();



            UserService us = new();

            var t1 = us.GetSingleTestAsync(new Test { Id = 6 }).Result;
            var t2 = us.GetAllTestsAsync(true).Result;

            /*

            var iu = us.RemoveTestAsync(new Test { TestName = "New test" });


            int y = 1;
            Test t = us.GetSingleTestAsync(new Test { Id = 6 }).Result;

            var q1 = us.GetSingleQuestionAsync(new Question { QuestionText = "New Quest 1" });
            var q2 = us.GetSingleQuestionAsync(new Question { QuestionText = "New Quest 2" });


            var y2 = us.UpdateTestAsync(new Test());


            Test test = new()
            {
                TestName = "New test",
                Questions = new List<Question>
                {
                  q1.Result, q2.Result
                   //new (){ QuestionText = "New Quest 1", Answers = new List<Answer>
                   //      {             
                   //          new() {AnswerText = "New Answer 1_1", IsCorrect = true } ,
                   //           new() {AnswerText = "New Answer 1_2", IsCorrect = false }
                   //      }
                   //},
                         
                   //new (){ QuestionText = "New Quest 2", Answers = new List<Answer>
                   //      {            
                   //          new() {AnswerText = "New Answer 2_1", IsCorrect = true } ,
                   //           new() {AnswerText = "New Answer 2_2", IsCorrect = false }
                   //      }
                   //}              
                }
            };

            var y22 = us.AddNewTestAsync(test);
            */

            //CreateHostBuilder(args).Build().Run();
        }

        //public static IHostBuilder CreateHostBuilder(string[] args) =>
        //    Host.CreateDefaultBuilder(args)
        //        .ConfigureWebHostDefaults(webBuilder =>
        //        {
        //            webBuilder.UseStartup<Startup>();
        //        });
    }
}
