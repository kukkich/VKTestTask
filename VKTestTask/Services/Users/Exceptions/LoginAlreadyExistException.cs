using VKTestTask.Exceptions;

namespace VKTestTask.Services.Users.Exceptions;

public class LoginAlreadyExistException : ApiException
{
    public override string Message => $"Login {_login} already exist";

    private readonly string _login;

    public LoginAlreadyExistException(string login)
    {
        _login = login;
    }
}