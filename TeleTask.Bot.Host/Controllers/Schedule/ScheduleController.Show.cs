using Deployf.Botf;
using TeleTask.Application.MediatR.Schedule;
using TeleTask.Common;

namespace TeleTask.Bot.Host.Controllers.Schedule;

public partial class ScheduleController
{
    [Action]
    public async Task Show()
    {
        var schedules = await _sender.Send(new ShowSchedule.Request
        {
            UserId = FromId
        });

        var table = schedules.ToStringTable(["Id", "Name", "   Deadline"],
            a => a.Id, a => a.Name, a => $"{a.Deadline:dd.MM.yy HH:mm}");
        
        
        Button("Add new", Q(StartAddProcess));
        Button("Go back", "/start");

        await Update(text: $"<code>{table}</code>");
    }
}
