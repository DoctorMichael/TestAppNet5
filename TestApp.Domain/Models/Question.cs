using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
