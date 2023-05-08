namespace VKTestTask.Services.Delay;

public class FixedTimeDelayTimeService : IDelayTimeService
{
    private readonly int _delay;

    public FixedTimeDelayTimeService(int delay)
    {
        _delay = delay;
    }

    public int GetDelay() => _delay;
}