namespace VKTestTask.Services.Auth;

public class AdminOnlyAuthorizationService : IAuthorizationService
{
    public bool HasAccess(string username, string password)
    {
        return username == "admin" && password == "123";
    }
}