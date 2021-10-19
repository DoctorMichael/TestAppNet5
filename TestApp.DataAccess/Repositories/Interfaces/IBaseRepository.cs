using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestApp.DataAccess.Context;

namespace TestApp.DataAccess.Repositories.Interfaces
{
    public interface IBaseRepository
    {
        TestAppContext Context { get;}
    }
}
