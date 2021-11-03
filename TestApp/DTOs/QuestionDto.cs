using System.Collections.Generic;
using TestApp.Domain.Models;

namespace TestApp.DTOs
{
    public class QuestionDto
    {
        public int Id { get; set; }
        public string QuestionText { get; set; }
        public List<AnswerDto> Answers { get; set; } = new();

        public QuestionDto() { }

        public QuestionDto(Question question)
        {
            Id = question.Id;
            QuestionText = question.QuestionText;

            if (question.Answers != null)
            {
                foreach (var item in question.Answers)
                {
                    Answers.Add(new AnswerDto(item));
                }
            }
        }
    }
}
