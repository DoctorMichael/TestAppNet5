using System.Collections.Generic;
using TestApp.Domain.Models;

namespace TestApp.DTOs
{
    public class UserDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Password { get; set; }
        public bool IsController { get; set; }
        public List<UserAnswerDto> UserAnswers { get; set; } = new();

        public UserDto() { }
        public UserDto(User user)
        {
            Id = user.Id;
            Name = user.Name;
            Password = user.Password;
            IsController = user.IsController;

            if (user.UserAnswers != null)
            {
                foreach (var item in user.UserAnswers)
                {
                    UserAnswers.Add(new UserAnswerDto(item));
                }
            }
        }
    }
}
