using System.Collections.Generic;

namespace TestApp.DTOs
{
    public class CreateTestDto
    {
        public string TestName { get; set; }
        public List<int> QuestionIds { get; set; } = new();
    }
}
