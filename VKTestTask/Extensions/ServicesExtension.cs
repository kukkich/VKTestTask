using VKTestTask.Repositories.Users;
using VKTestTask.Services.Auth;
using VKTestTask.Services.Delay;
using VKTestTask.Services.Reservation;
using VKTestTask.Services.Time;
using VKTestTask.Services.Users;

namespace VKTestTask.Extensions;

public static class ServicesExtension
{
    public static void AddDomain(this IServiceCollection services)
    {
        AddDomainServices(services);
        AddRepositories(services);
    }

    private static void AddDomainServices(IServiceCollection services)
    {
        services.AddScoped<IUserService, UserService>();
    }

    private static void AddRepositories(IServiceCollection services)
    {
        services.AddScoped<IUserRepository, UserDbRepository>();
    }

    public static void AddHelpers(this IServiceCollection services)
    {
        services.AddSingleton<ITimeService, CurrentTimeService>();
        services.AddSingleton<ILoginReservationService, InMemoryLoginReservationService>();
        services.AddSingleton<IDelayTimeService, FixedTimeDelayTimeService>(_ => new FixedTimeDelayTimeService(5000));
        services.AddSingleton<IAuthorizationService, AdminOnlyAuthorizationService>();
    }
}