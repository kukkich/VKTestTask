using VKTestTask.Exceptions;

namespace VKTestTask.Services.Users.Exceptions;

public class LoginReservedException : ApiException
{
    public override string Message => $"Login {_login} reserved";

    private readonly string _login;

    public LoginReservedException(string login)
    {
        _login = login;
    }
}