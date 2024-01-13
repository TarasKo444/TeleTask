using Telegram.Bot;
using TeleTask.Host;

var builder = Host.CreateApplicationBuilder(args);

builder.Services.AddSingleton(new TelegramBotClient(builder.Configuration["BotSettings:Token"]!));

builder.Services.AddHostedService<BotStartup>();

builder.Logging.AddConsole();

var host = builder.Build();
host.Run();