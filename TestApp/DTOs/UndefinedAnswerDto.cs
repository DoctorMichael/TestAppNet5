using TestApp.Domain.Models;

namespace TestApp.DTOs
{
    public class UndefinedAnswerDto
    {
        public int Id { get; set; }
        public string AnswerText { get; set; }
        public int QuestionId { get; set; }

        public UndefinedAnswerDto() { }

        public UndefinedAnswerDto(Answer answer)
        {
            Id = answer.Id;
            AnswerText = answer.AnswerText;
            QuestionId = answer.QuestionId;
        }
    }
}
