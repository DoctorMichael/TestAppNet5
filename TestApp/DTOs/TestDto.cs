using System.Collections.Generic;
using TestApp.Domain.Models;

namespace TestApp.DTOs
{
    public class TestDto
    {
        public int Id { get; set; }
        public string TestName { get; set; }
        public List<QuestionDto> Questions { get; set; } = new();

        public TestDto() { }

        public TestDto(Test test)
        {
            Id = test.Id;
            TestName = test.TestName;

            if (test.Questions != null)
            {
                foreach (var item in test.Questions)
                {
                    Questions.Add(new QuestionDto(item));
                }
            }
        }
    }
}
