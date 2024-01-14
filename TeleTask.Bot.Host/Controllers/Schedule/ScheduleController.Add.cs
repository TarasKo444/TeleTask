using System.Globalization;
using Deployf.Botf;
using TeleTask.Application.MediatR.Schedule;

namespace TeleTask.Bot.Host.Controllers.Schedule;

public partial class ScheduleController
{
    [Action]
    public async Task StartAddProcess()
    {
        Push("Name of the task?");

        await Update();

        var name = await AwaitText();

        YearSelect("", name);
        await Send();
    }

    [Action]
    public async Task FinishCreating(string dt, string name)
    {
        var datetime = DateTime.FromBinary(dt.Base64());
        Button("Add new", Q(StartAddProcess));
        Button("Go back", "/start");
        Push($"Task due to {datetime.ToString(CultureInfo.CurrentCulture)} created \u2705");
        await SendOrUpdate();

        await _sender.Send(new AddSchedule.Request
        {
            Deadline = datetime,
            Name = name,
            UserId = FromId
        });
    }
}
