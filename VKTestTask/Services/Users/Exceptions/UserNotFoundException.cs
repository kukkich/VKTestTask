using VKTestTask.Exceptions;

namespace VKTestTask.Services.Users.Exceptions;

public class UserNotFoundException : ApiException
{
    public override string Message => $"No user with id {_userId}";

    private readonly int _userId;

    public UserNotFoundException(int userId)
    {
        _userId = userId;
    }
}