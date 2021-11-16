using TestApp.Domain.Models;

namespace TestApp.DTOs
{
    public class AddUserDto
    {
        public string Name { get; set; }
        public string Password { get; set; }
        public bool IsController { get; set; }

        public AddUserDto() { }
        public AddUserDto(UserDto userDto)
        {
            Name = userDto.Name;
            Password = userDto.Password;
            IsController = userDto.IsController;
        }
        public AddUserDto(User user)
        {
            Name = user.Name;
            Password = user.Password;
            IsController = user.IsController;
        }
    }
}
