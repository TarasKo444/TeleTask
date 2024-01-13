using Telegram.Bot;
using Telegram.Bot.AttributeCommands;
using Telegram.Bot.AttributeCommands.Exceptions;
using Telegram.Bot.Exceptions;
using Telegram.Bot.Polling;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using TeleTask.Host.Extensions;

namespace TeleTask.Host;

public class BotStartup : IHostedService
{
    private readonly TelegramBotClient _botClient;
    private readonly ILogger<BotStartup> _logger;
    private readonly ReceiverOptions _receiverOptions;
    private readonly AttributeCommands _commands;

    public BotStartup(TelegramBotClient botClient, ILogger<BotStartup> logger)
    {
        _botClient = botClient;
        _logger = logger;
        
        _commands = new AttributeCommands();
        _commands.RegisterCommands();
        
        _receiverOptions = new()
        {
            AllowedUpdates = Array.Empty<UpdateType>() // receive all update types except ChatMember related updates
        };
    }

    public Task StartAsync(CancellationToken cancellationToken)
    {
        _botClient.StartReceiving(
            updateHandler: HandleUpdateAsync,
            pollingErrorHandler: HandlePollingErrorAsync,
            receiverOptions: _receiverOptions,
            cancellationToken: cancellationToken
        );

        _logger.LogInformation("{}", "Telegram bot started receiving");
        
        return Task.CompletedTask;
    }

    private Task HandlePollingErrorAsync(ITelegramBotClient botClient, Exception exception,
        CancellationToken cancellationToken)
    {
        var ErrorMessage = exception switch
        {
            ApiRequestException apiRequestException 
                => $"Telegram API Error:\n[{apiRequestException.ErrorCode}]\n{apiRequestException.Message}",
            _ => exception.ToString()
        };

        _logger.LogInformation("{}", ErrorMessage);
        
        return Task.CompletedTask;
    }

    private Task HandleUpdateAsync(ITelegramBotClient botClient, Update update,
        CancellationToken cancellationToken)
    {
        if (update.Message is not { Text: { } messageText } message) return Task.CompletedTask;
        
        var chatId = message.Chat.Id;
        var userId = message.From!.Id;

        _logger.LogInformation("{}", $"Received a '{messageText}' message in chat {chatId} from {userId}.");
        
        return BotOnMessageReceived(update);
    }
    
    private async Task BotOnMessageReceived(Update update)
    {
        try
        {
            await _commands.ProcessCommand(update.Message!.Text!, new object[] { _botClient, update });
        }
        catch (CommandNotFoundException)
        {
            // await _botClient.SendTextMessageAsync(update.Message!.Chat.Id, ex.Message);
        }
    }

    public async Task StopAsync(CancellationToken cancellationToken)
    {
        await _botClient.DeleteWebhookAsync(cancellationToken: cancellationToken);
        await _botClient.CloseAsync(cancellationToken: cancellationToken);
    }
}