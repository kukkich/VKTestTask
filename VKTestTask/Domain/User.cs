namespace VKTestTask.Domain;

public class User
{
    public int Id { get; set; }
    public string Login { get; set; } = null!;
    public string Password { get; set; } = null!;
    public DateTime CreatedDate { get; set; }

    public int UserGroupId { get; set; }
    public UserGroup UserGroup { get; set; } = null!;

    public int UserStateId { get; set; }
    public UserState UserState { get; set; } = null!;
}