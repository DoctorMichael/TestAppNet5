using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestApp.BusinessLogic.Services.Interfaces;
using TestApp.Domain.Models;

namespace TestApp.BusinessLogic.Services.Implementation
{
    public class UserService : IUserService
    {
        public Task<Answer> AddAnswerAsync(Answer answer)
        {
            throw new NotImplementedException();
        }

        public Task<Question> AddQuestionAsync(string questionText)
        {
            throw new NotImplementedException();
        }

        public Task<User> AuthenticateUserAsync(string name, string password)
        {
            throw new NotImplementedException();
        }

        public Task<Answer> GetAnswerAsync(Question question)
        {
            throw new NotImplementedException();
        }

        public Task<Test> GetQuestionListAsync(Test test)
        {
            FakeDB fdb = new FakeDB();

            if (test.Id == 1)
            {
                return Task.FromResult(new Test { Questions = { fdb.questionTable[0], fdb.questionTable[1], fdb.questionTable[2] } });
            }
            else if (test.Id == 2)
            {
                return Task.FromResult(new Test { Questions = { fdb.questionTable[3], fdb.questionTable[4] } });
            }
            else if (test.Id == 3)
            {
                return Task.FromResult(new Test { Questions = { fdb.questionTable[5]} });
            }
            else
                return null;
        }

        public Task<List<Test>> GetTestListAsync()
        {
            return Task.FromResult(new FakeDB().testTable);
        }

        public Task<User> GetUserAnswerSetAsync(User user)
        {
            throw new NotImplementedException();
        }

        public Task<List<User>> GetUserListAsync(User user)
        {

            if (user.IsController)
            {
                return Task.FromResult(new FakeDB().userTable);
            }

            return null;
        }

        public Task<User> RegisterUserAsync(string name, string password)
        {
            throw new NotImplementedException();
        }

        public Task<bool> RemoveAnswerAsync(Answer answer)
        {
            throw new NotImplementedException();
        }

        public Task<bool> RemoveQuestionAsync(Question question)
        {
            throw new NotImplementedException();
        }
    }
}
