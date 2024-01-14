

using Deployf.Botf;
using TeleTask.Application;
using TeleTask.Bot.Host.Options;
using TeleTask.Infrastructure;

BotfProgram.StartBot(args, onConfigure: (services, configuration) =>
{
    services.Configure<TelegramOptions>(configuration.GetSection("Telegram"));
    
    services.AddInfrastructure(configuration);
    services.AddApplication();
});