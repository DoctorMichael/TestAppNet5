using TestApp.Domain.Models;

namespace TestApp.DTOs
{
    public class UserAnswerDto
    {
        public int UserID { get; set; }
        public int TestID { get; set; }
        public int AnswerID { get; set; }

        public UserAnswerDto() { }

        public UserAnswerDto(UserAnswer userAnswer)
        {
            UserID = userAnswer.UserID;
            TestID = userAnswer.TestID;
            AnswerID = userAnswer.AnswerID;
        }
    }
}
