using Deployf.Botf;
using TeleTask.Application.MediatR.Schedule;

namespace TeleTask.Bot.Host.Controllers.Schedule;

public partial class ScheduleController
{
    [Action]
    public async Task StartEndProcess()
    {
        Push("Id of the task?");

        await Update();

        var input = await AwaitText();

        if(input == "/start") return;
        
        var ids = input.Split();

        foreach (var id in ids)
        {
            if (long.TryParse(id, out var idLong))
            {
                await _sender.Send(new EndSchedule.Request
                {
                    Id = idLong
                });   
            }
        }
        
        Button("Add new", Q(StartAddProcess));
        Button("Go back", "/start");
        Push("Schedule successfully deleted \u2705");
        
        await Send();
    }
}
