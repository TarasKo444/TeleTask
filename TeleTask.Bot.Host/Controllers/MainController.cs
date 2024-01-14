using Deployf.Botf;

namespace TeleTask.Bot.Host.Controllers;

public class MainController : BotController
{
    [Action("/start", "Starts bot")]
    public void Start()
    {
        PushL("Welcome, choose action");
        Button("Add", Q(AddButton));   
        Button("Show", Q(ShowButton));
    }
    
    [Action]
    private async Task AddButton()
    {
        Push("Added");
        await Send();
    }
    
    [Action]
    private async Task ShowButton()
    {
        PushL("Showed");
        await Send();
    }
    
}
