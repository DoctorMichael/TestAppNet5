using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestApp.Domain.Models
{
    public class UserAnswer
    {
        public int UserID { get; set; }
        public User User { get; set; }

        public int TestID { get; set; }
        public Test Test { get; set; }

        public int AnswerID { get; set; }
        public Answer Answer { get; set; }
    }
}
