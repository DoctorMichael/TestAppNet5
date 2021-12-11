using System.Collections.Generic;
using System.Linq;
using TestApp.Domain.Models;

namespace TestApp.DTOs
{
    public class CheckUserAnswersForTestDto
    {
        public int UserId { get; set; }
        public string UserName { get; set; } = "n/a";
        public int TestId { get; set; }
        public string TestName { get; set; }

        public List<QuestionAndUserAnswerDto> QuestAndUserAnswerDtos { get; set; } = new();

        public CheckUserAnswersForTestDto() { }

        public CheckUserAnswersForTestDto(ICollection<Question> questions)
        {
            if (questions != null)
            {
                foreach (var item in questions)
                {
                    if (item.Answers != null)
                    {
                        var itemAnswer = item.Answers.Last();

                        QuestAndUserAnswerDtos.Add(new QuestionAndUserAnswerDto()
                        {
                            QuestionId = item.Id,
                            QuestionText = item.QuestionText,
                            AnswerId = itemAnswer.Id,
                            AnswerText = itemAnswer.AnswerText,
                            AnswerIsCorrect = itemAnswer.IsCorrect
                        });
                    }
                }
            }
        }
    }
}
