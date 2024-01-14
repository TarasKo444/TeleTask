namespace TeleTask.Domain;

public class Schedule
{
    public long Id { get; set; }
    public string Name { get; set; } = null!;
    public DateTime Deadline { get; set; }

    public long UserId { get; set; }
    public User User { get; set; } = null!;
}
