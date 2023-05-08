namespace VKTestTask.Services.Auth;

public interface IAuthorizationService
{
    public bool HasAccess(string username, string password);
}