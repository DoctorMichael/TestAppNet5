using System.Collections.Generic;

namespace TestApp.Domain.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Password { get; set; }
        public bool IsController { get; set; }

        public ICollection<UserAnswer> UserAnswers { get; set; }
    }
}
