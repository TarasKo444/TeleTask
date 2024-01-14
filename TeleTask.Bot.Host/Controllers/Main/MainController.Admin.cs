using Deployf.Botf;

namespace TeleTask.Bot.Host.Controllers.Main;

public partial class MainController
{
    [Action]
    private async Task AdminButton()
    {
        Push("Admin");
        Button("Go back", "/start");

        await Update();
    }
}
