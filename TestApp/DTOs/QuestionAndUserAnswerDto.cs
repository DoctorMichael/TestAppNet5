namespace TestApp.DTOs
{
    public class QuestionAndUserAnswerDto
    {
        public int QuestionId { get; set; }
        public string QuestionText { get; set; }
  
        public int AnswerId { get; set; }
        public string AnswerText { get; set; }
        public bool AnswerIsCorrect { get; set; }
    }
}
