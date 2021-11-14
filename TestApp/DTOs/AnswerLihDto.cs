using TestApp.Domain.Models;

namespace TestApp.DTOs
{
    public class AnswerLightDto
    {
        public string AnswerText { get; set; }
        public bool IsCorrect { get; set; }

        public AnswerLightDto() { }

        public AnswerLightDto(AnswerDto answerDto)
        {
            AnswerText = answerDto.AnswerText;
            IsCorrect = answerDto.IsCorrect;
        }

        public AnswerLightDto(Answer answer)
        {
            AnswerText = answer.AnswerText;
            IsCorrect = answer.IsCorrect;
        }
    }
}
