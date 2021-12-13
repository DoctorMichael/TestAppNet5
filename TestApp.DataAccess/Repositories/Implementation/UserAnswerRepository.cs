using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestApp.DataAccess.Context;
using TestApp.DataAccess.Repositories.Interfaces;
using TestApp.Domain.Models;

namespace TestApp.DataAccess.Repositories.Implementation
{
    public class UserAnswerRepository : BaseRepository<UserAnswer, TestAppContext>, IUserAnswerRepository, IBaseRepository<UserAnswer>
    {
        protected readonly DbSet<UserAnswer> _dbSetUserAnswer;

        public UserAnswerRepository(TestAppContext context) : base(context)
        {
            _dbSetUserAnswer = context.Set<UserAnswer>();
        }
        public async Task<IEnumerable<UserAnswer>> GetUserAnswersForTestAsync(int userId, int testId)
        {
            return await _dbSetUserAnswer.AsNoTracking()
                                         .Where(u => u.UserID == userId && u.TestID == testId)
                                         .ToListAsync();
        }

        public async Task<UserAnswer> AddNewUserAnswerAsync(UserAnswer userAnswer)
        {
            var res = await _dbSetUserAnswer.AddAsync(userAnswer);
            return res.Entity;
        }
    }
}
