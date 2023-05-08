namespace VKTestTask.Domain;

public class UserState
{
    public int Id { get; set; }
    public StateCode Code { get; set; }
    public string DisplayCode => Enum.GetName(Code)!;
    public string Description { get; set; } = null!;
}