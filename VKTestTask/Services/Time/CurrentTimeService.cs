namespace VKTestTask.Services.Time;

public class CurrentTimeService : ITimeService
{
    public DateTime GetTime() => DateTime.Now; 
}