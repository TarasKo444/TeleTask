using Deployf.Botf;
using Telegram.Bot.Types.Enums;
using TeleTask.Application.MediatR.User;

namespace TeleTask.Bot.Host.Controllers.Main;

public partial class MainController
{
    [On(Handle.BeforeAll)]
    public async Task PreHandler()
    {
        await _sender.Send(new AddUser.Request
        {
            Id = FromId,
            Username = Context.GetUsername()!
        });
    }
    
    [On(Handle.Exception)]
    public void Ex(Exception e)
    {
        _logger.LogCritical(e, "Unhandled exception");

        switch (Context.Update.Type)
        {
            case UpdateType.CallbackQuery:
                Push("Error");
                break;
            case UpdateType.Message:
                Push("Error");
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
    }
    
    [On(Handle.ChainTimeout)]
    public async Task ChainTimeout()
    {
        await Send("Timeout");
    }
}
