using System.Collections.Generic;

namespace TestApp.Domain.Models
{
    public class Question
    {
        public int Id { get; set; }
        public string QuestionText { get; set; }

        public ICollection<Answer> Answers { get; set; }
        public ICollection<Test> Tests { get; set; }
    }
}
