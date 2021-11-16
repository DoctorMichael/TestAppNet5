
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using TestApp.DataAccess.Context;
using TestApp.DataAccess.Repositories.Interfaces;
using TestApp.Domain.Models;

namespace TestApp.DataAccess.Repositories.Implementation
{
    public class QuestionRepository : BaseRepository<Question, TestAppContext>, IQuestionRepository, IBaseRepository<Question>
    {
        protected readonly DbSet<Question> _dbSetQuestion;

        public QuestionRepository(TestAppContext context) : base(context)
        {
            _dbSetQuestion = context.Set<Question>();
        }

        public async Task<IEnumerable<Question>> GetAllQuestionsAsync(bool includeAnswers)
        {
            if (includeAnswers)
                return await _dbSetQuestion.AsNoTracking()
                                           .Include(q => q.Answers)
                                           .ToListAsync();
            else
                return await _dbSetQuestion.AsNoTracking()
                                           .ToListAsync();
        }

        public async Task<Question> GetSingleQuestionAsync(int questionId)
        {
            return await _dbSetQuestion.Include(q => q.Answers)
                                       .FirstOrDefaultAsync(q => q.Id == questionId);
        }

        public async Task<Question> AddNewQuestionAsync(Question question)
        {
            var res = await _dbSetQuestion.AddAsync(question);
            return res.Entity;
        }
    }
}
