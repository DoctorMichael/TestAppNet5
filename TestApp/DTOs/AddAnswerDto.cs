using TestApp.Domain.Models;

namespace TestApp.DTOs
{
    public class AddAnswerDto
    {
        public string AnswerText { get; set; }
        public bool IsCorrect { get; set; }

        public AddAnswerDto() { }

        public AddAnswerDto(AnswerDto answerDto)
        {
            AnswerText = answerDto.AnswerText;
            IsCorrect = answerDto.IsCorrect;
        }

        public AddAnswerDto(Answer answer)
        {
            AnswerText = answer.AnswerText;
            IsCorrect = answer.IsCorrect;
        }
    }
}
