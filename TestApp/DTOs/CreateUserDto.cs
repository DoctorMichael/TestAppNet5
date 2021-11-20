using TestApp.Domain.Models;

namespace TestApp.DTOs
{
    public class CreateUserDto
    {
        public string Name { get; set; }
        public string Password { get; set; }
        public bool IsController { get; set; }

        public CreateUserDto() { }
        public CreateUserDto(UserDto userDto)
        {
            Name = userDto.Name;
            Password = userDto.Password;
            IsController = userDto.IsController;
        }
        public CreateUserDto(User user)
        {
            Name = user.Name;
            Password = user.Password;
            IsController = user.IsController;
        }
    }
}
