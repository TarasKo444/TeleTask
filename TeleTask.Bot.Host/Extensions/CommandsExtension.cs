using Telegram.Bot.AttributeCommands;
using TeleTask.Host.Commands;

namespace TeleTask.Host.Extensions;

public static class CommandsExtension
{
    public static void RegisterCommands(this AttributeCommands commands)
    {
        var discoverCommands = DiscoverCommands();

        foreach (var discoverCommand in discoverCommands)
        {
            commands.RegisterCommands(discoverCommand.GetType());
        }
    }

    private static IEnumerable<ICommand> DiscoverCommands()
    {
        return typeof(ICommand).Assembly
            .GetTypes()
            .Where(p => p.IsClass && p.IsAssignableTo(typeof(ICommand)))
            .Select(Activator.CreateInstance)
            .Cast<ICommand>();
    }
}
