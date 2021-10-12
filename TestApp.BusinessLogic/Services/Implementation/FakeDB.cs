using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestApp.Domain.Models;

namespace TestApp.BusinessLogic.Services.Implementation
{
    public class FakeDB
    {
        public List<User> userTable = new List<User>()
        {
            new User{Id = 1}
            //new User{ Id = 1, Name = "Mike", Password = "1111", IsController = true },
            //new User() { Id = 2, Name = "Ann", Password = "2222", IsController = false }
        };


        public List<Test> testTable = new List<Test>()
        {
            new Test() { Id = 1, TestName = "Mathematics" },
            new Test() { Id = 2, TestName = "English" },
            new Test() { Id = 3, TestName = "Pretend To Be Kind" }
        };


        public List<Question> questionTable = new List<Question>()
        {
            new Question() { Id = 1, QuestionText = "The Nearest Result for 2 x 2 = ..."},
            new Question() { Id = 2, QuestionText = "0 / 0 = ..."},
            new Question() { Id = 3, QuestionText = "(-1)^(1 / 2) = ..."},
            new Question() { Id = 4, QuestionText = "How Old Are You?"},
            new Question() { Id = 5, QuestionText = "One  Two  ...  Four"},
            new Question() { Id = 6, QuestionText = "Are You Sorry For Moo-moo?"}
        };
    }
}
