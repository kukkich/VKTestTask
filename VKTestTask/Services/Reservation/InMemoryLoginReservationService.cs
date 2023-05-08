using System.Collections.Concurrent;

namespace VKTestTask.Services.Reservation;

public class InMemoryLoginReservationService : ILoginReservationService
{
    private readonly ConcurrentDictionary<string, bool> _reserved = new();

    public void Reserve(string login)
    {
        var isSuccessfullyAdded = _reserved.TryAdd(login, default);

        if (!isSuccessfullyAdded)
            throw new InvalidOperationException($"Login {login} was reserved");
    }

    public void Cancel(string login)
    {
        var isSuccessfullyRemoved = _reserved.TryRemove(login, out _);

        if (!isSuccessfullyRemoved)
            throw new InvalidOperationException($"No reserved login {login}");
    }

    public bool IsReserved(string login)
    {
        return _reserved.ContainsKey(login);
    }
}