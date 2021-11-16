
using System.Collections.Generic;
using System.Threading.Tasks;
using TestApp.Domain.Models;

namespace TestApp.DataAccess.Repositories.Interfaces
{
    public interface IQuestionRepository : IBaseRepository<Question>
    {
        Task<IEnumerable<Question>> GetAllQuestionsAsync(bool includeAnswers);
        Task<Question> GetSingleQuestionAsync(int questionId);
        Task<Question> AddNewQuestionAsync(Question question);
    }
}
