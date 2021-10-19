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
    public class BaseRepository : IBaseRepository
    {

        protected readonly TestAppContext context;

        public BaseRepository()
        {
            context = new TestAppContext(new DbContextOptions<TestAppContext>());
        }
        TestAppContext IBaseRepository.Context { get => context; }
    }
}
