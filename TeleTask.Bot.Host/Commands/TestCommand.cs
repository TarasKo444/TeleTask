﻿using Telegram.Bot;
using Telegram.Bot.AttributeCommands.Attributes;
using Telegram.Bot.Types;

namespace TeleTask.Host.Commands;

public class TestCommand : ICommand
{
    [TextCommand("/add")]
    public static async Task Execute(TelegramBotClient client, Update update)
    {
        var chatId = update.Message!.Chat.Id;
        
        await client.SendTextMessageAsync(
            chatId: chatId,
            text: "Added");
    }
}
