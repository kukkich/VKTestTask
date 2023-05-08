namespace VKTestTask.Domain.Dto;

public class UserDto
{
    public int Id { get; set; }
    public string Login { get; set; }
    public string Password { get; set; }
    public DateTime CreatedDate { get; set; }

    public UserGroup UserGroup { get; set; }
    public UserState UserState { get; set; }

    public UserDto(User user)
    {
        Id = user.Id;
        Login = user.Login;
        Password = user.Password;
        CreatedDate = user.CreatedDate;
        UserGroup = user.UserGroup;
        UserState = user.UserState;
    }

    public static explicit operator UserDto(User user)
    {
        return new UserDto(user);
    }
}