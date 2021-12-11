using TestApp.Domain.Models;

namespace TestApp.DTOs
{
    public class AnswerDto
    {
        public int Id { get; set; }
        public string AnswerText { get; set; }
        public int QuestionId { get; set; }

        public AnswerDto() { }

        public AnswerDto(Answer answer)
        {
            Id = answer.Id;
            AnswerText = answer.AnswerText;
            QuestionId = answer.QuestionId;
        }
    }
}
