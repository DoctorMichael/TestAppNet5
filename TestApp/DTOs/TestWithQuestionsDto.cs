using System.Collections.Generic;

namespace TestApp.DTOs
{
    public class TestWithQuestionsDto
    {
        public int TestId {  get; set; }
        public List<int> QuestionIds { get; set; } = new();
    }
}
