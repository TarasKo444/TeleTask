using Deployf.Botf;

namespace TeleTask.Bot.Host.Controllers.Schedule;

public partial class ScheduleController
{
    [Action]
    public void YearSelect(string state, string name)
    {
        var now = DateTime.Now;
        new CalendarMessageBuilder()
            .Year(now.Year).Month(now.Month).Day(now.Day)
            .Depth(CalendarDepth.ToMinutes)
            .SetState(state)

            .OnNavigatePath(s => Q(YearSelect, s, name))
            .OnSelectPath(d => Q(FinishCreating, d.ToBinary().Base64(), name))
            
            .SkipYear(y => y < DateTime.Now.Year)

            .FormatMinute(d => $"{d:HH:mm}")
            .FormatText((dt, depth, b) => {
                b.PushL($"Select {depth}");
                b.PushL($"Current state: {dt}");
            })

            .Build(Message, new PagingService());
    }
}
