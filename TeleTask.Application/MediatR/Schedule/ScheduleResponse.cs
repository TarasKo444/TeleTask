namespace TeleTask.Application.MediatR.Schedule;

public class ScheduleResponse
{
    public long Id { get; set; }
    public string Name { get; set; } = null!;
    public DateTime Deadline { get; set; }
    public long UserId { get; set; }
}
