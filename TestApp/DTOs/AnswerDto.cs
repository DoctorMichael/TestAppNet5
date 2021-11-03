using System.Collections.Generic;
using TestApp.Domain.Models;

namespace TestApp.DTOs
{
    public class AnswerDto
    {
        public int Id { get; set; }
        public string AnswerText { get; set; }
        public bool IsCorrect { get; set; }
        public int QuestionId { get; set; }

        public AnswerDto() { }

        public AnswerDto(Answer answer)
        {
            Id = answer.Id;
            AnswerText = answer.AnswerText;
            IsCorrect = answer.IsCorrect;
            QuestionId = answer.QuestionId;
        }
    }
}
