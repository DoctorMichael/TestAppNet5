using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
