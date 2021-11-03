using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
    }
}
