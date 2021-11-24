using System.Collections.Generic;

namespace TestApp.DTOs
{
    public class CreateQuestionDto
    {
        public string QuestionText { get; set; }
        public List<CreateAnswerDto> CreateAnswerDtos { get; set; } = new();
    }
}
