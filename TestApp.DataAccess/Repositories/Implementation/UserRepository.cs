using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using TestApp.DataAccess.Context;
using TestApp.DataAccess.Repositories.Interfaces;
using TestApp.Domain.Models;

namespace TestApp.DataAccess.Repositories.Implementation
{
    public class UserRepository : BaseRepository<User, TestAppContext>, IUserRepository, IBaseRepository<User>
    {
        protected readonly DbSet<User> _dbSetUser;
        public UserRepository(TestAppContext context) : base(context)
        {
            _dbSetUser = context.Set<User>();
        }

        public async Task<IEnumerable<User>> GetAllUsersAsync(bool includeUserAnswers)
        {
            if (includeUserAnswers)
                return await _dbSetUser.AsNoTracking()
                                       .Include(u => u.UserAnswers)
                                       .ToListAsync();
            else
                return await _dbSetUser.AsNoTracking()
                                       .ToListAsync();
        }

        public async Task<User> GetSingleUserAsync(int userId)
        {
            return await _dbSetUser.Include(u => u.UserAnswers)
                                   .FirstOrDefaultAsync(u => u.Id == userId);
        }

        public async Task<User> AddNewUserAsync(User user)
        {
            var res = await _dbSetUser.AddAsync(user);
            return res.Entity;
        }
    }
}
