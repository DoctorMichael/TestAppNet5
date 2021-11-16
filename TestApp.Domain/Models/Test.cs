using System.Collections.Generic;

namespace TestApp.Domain.Models
{
    public class Test
    {
        public int Id { get; set; }
        public string TestName { get; set; }
       
        public ICollection<Question> Questions { get; set; }
        public ICollection<UserAnswer> UserAnswers { get; set; }
    }
}
