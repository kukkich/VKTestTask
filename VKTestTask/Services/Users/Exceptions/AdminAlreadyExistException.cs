using VKTestTask.Exceptions;

namespace VKTestTask.Services.Users.Exceptions;

public class AdminAlreadyExistException : ApiException
{
    public override string Message => "Admin already exist";
}