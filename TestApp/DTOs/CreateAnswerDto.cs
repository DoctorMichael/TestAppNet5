using TestApp.Domain.Models;

namespace TestApp.DTOs
{
    public class CreateAnswerDto
    {
        public string AnswerText { get; set; }
        public bool IsCorrect { get; set; }

        public CreateAnswerDto() { }

        public CreateAnswerDto(AnswerDto answerDto)
        {
            AnswerText = answerDto.AnswerText;
            IsCorrect = answerDto.IsCorrect;
        }

        public CreateAnswerDto(Answer answer)
        {
            AnswerText = answer.AnswerText;
            IsCorrect = answer.IsCorrect;
        }
    }
}
