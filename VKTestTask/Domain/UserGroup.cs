namespace VKTestTask.Domain;

public class UserGroup
{
    public int Id { get; set; }
    public GroupCode Code { get; set; }
    public string DisplayCode => Enum.GetName(Code)!;
    public string Description { get; set; } = null!;
}