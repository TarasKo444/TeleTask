using System.Globalization;
using System.Text;
using Deployf.Botf;
using MediatR;
using Telegram.Bot.Types.Enums;
using TeleTask.Application.MediatR.Schedule;
using TeleTask.Common;

namespace TeleTask.Bot.Host.Controllers.Schedule;

public partial class ScheduleController : BotController
{
    private readonly ISender _sender;

    public ScheduleController(ISender sender)
    {
        _sender = sender;
    }
}