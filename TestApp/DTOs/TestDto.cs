using System.Collections.Generic;
using TestApp.Domain.Models;

namespace TestApp.DTOs
{
    public class TestDto
    {
        public int Id { get; set; }
        public string TestName { get; set; }
        public ICollection<Question> Questions { get; set; }
        public ICollection<UserAnswer> UserAnswers { get; set; }
    }
}
