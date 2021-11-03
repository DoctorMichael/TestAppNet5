using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using TestApp.DataAccess.Context;
using TestApp.DataAccess.Repositories.Interfaces;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;

namespace TestApp.DataAccess.Repositories.Implementation
{
    public class BaseRepository<T, TContex> : IBaseRepository<T>
        where T : class
        where TContex : DbContext, IUnitOfWork
    {

        protected readonly TContex _context;

        public BaseRepository(TContex context)
        {
            _context = context;
        }

        public IUnitOfWork UnitOfWork { get => _context; }



        public async Task<T> CreateAsync(T item)
        {
            var entity = await _context.Set<T>().AddAsync(item);
            return entity.Entity;
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return await _context.Set<T>().AsNoTracking()
                                          .ToListAsync();
        }

        public Task Update(T item)
        {
            _context.Set<T>().Update(item);
            return Task.CompletedTask;
        }

        public Task Delete(T item)
        {
            _context.Set<T>().Remove(item);
            return Task.CompletedTask;
        }
    }

}
