using Telegram.Bot;
using Telegram.Bot.Exceptions;
using Telegram.Bot.Polling;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;

internal class Program
{
    private static async Task Main(string[] args)
    {
        var botClient = new TelegramBotClient("7900891678:AAFQGPBZ_BwCjx49MldyjPEKPqe-IaPZo6w");

        using CancellationTokenSource cts = new ();
        ReceiverOptions receiverOptions = new()
        {
            AllowedUpdates = Array.Empty<UpdateType>(),
        };

        botClient.StartReceiving(updateHandler:HandlerUpdateAsync,
            pollingErrorHandler: HadlerPollingErrorAsync,
            receiverOptions:receiverOptions,
            cancellationToken:cts.Token);


    var me=  await botClient.GetMeAsync();
        Console.WriteLine($"Hello, World! I am user {me.Id} and my name is {me.FirstName}.");
        Console.WriteLine($"Start listening for @{me.Username}");
        Console.ReadLine();
        cts.Cancel();


        async Task HandlerUpdateAsync(ITelegramBotClient client, Update update, CancellationToken cancellationToken)
        {

            if(update.Type==UpdateType.CallbackQuery)
            {
                await botClient.AnswerCallbackQueryAsync(callbackQueryId: update.CallbackQuery.Id,
                      text: $"Received {update.CallbackQuery.Data}");
            }
            if (update.Message is not { } message)
                return;

            if (message.Text is not { } messageText)
                return;
            var chatId = message.Chat.Id;
            Console.WriteLine($"Received a '{messageText}' message in chat {chatId}.");

            //await botClient.SendTextMessageAsync(chatId: chatId,
            //          text: "Hello",
            //          cancellationToken: cts.Token);

            switch (messageText)
            {
                case "/start":
                    string commands = @"Список команд: 
				/start - запуск бота
				/inline - меню
				/keyboard - вивід клавіатури";

                    await botClient.SendTextMessageAsync(
                        chatId: update.Message.Chat.Id,
                        text: commands,
                        cancellationToken: cancellationToken);
                    break;


                case "/inline":
                    var inlineKeyboard = new InlineKeyboardMarkup(new[] {
            new [] {
                InlineKeyboardButton.WithUrl("facebook", "https://www.facebook.com/"),
                InlineKeyboardButton.WithUrl("telegram", "https://web.telegram.org/")
            },
            new [] {
                InlineKeyboardButton.WithCallbackData("item1"),
                InlineKeyboardButton.WithCallbackData("item2"),
            },
        });

                    await botClient.SendTextMessageAsync(
                        chatId: update.Message.Chat.Id,
                        text: "Виберіть пункт меню: ",
                        replyMarkup: inlineKeyboard,
                        cancellationToken: cancellationToken
                        );
                    break;


                case "/keyboard":
                    ReplyKeyboardMarkup replyKeyboardMarkup = new(new[]
                    {
            new [] {
                new KeyboardButton ( "Привіт"),
                new KeyboardButton ( "Як справи?"),
            },
            new [] {
                new KeyboardButton ( "Контакт") {RequestContact = true },
                new KeyboardButton ( "Геолокація") {RequestLocation = true },
            },

        });


                    await botClient.SendTextMessageAsync(
                        chatId: update.Message.Chat.Id,
                        text: "Choose a response",
                        replyMarkup: replyKeyboardMarkup,
                        cancellationToken: cancellationToken);
                    break;

                default:
                    string answer = "Unrecognized command. Say what?";
                    await botClient.SendTextMessageAsync(
                        chatId: update.Message.Chat.Id,
                        text: answer,
                        cancellationToken: cancellationToken);

                    await botClient.SendPhotoAsync(
              chatId: chatId,
              photo: InputFile.FromUri("https://github.com/TelegramBots/book/raw/master/src/docs/photo-ara.jpg"),
              caption: "<b>Ara bird</b>. <i>Source</i>: <a href=\"https://pixabay.com\">Pixabay</a>",
              parseMode: ParseMode.Html,
              cancellationToken: cancellationToken);
                    break;



            }


        }
        Task HadlerPollingErrorAsync(ITelegramBotClient client, Exception exception, CancellationToken token)
        {
            var ErrorMessage = exception switch
            {
                ApiRequestException apiRequestException
                    => $"Telegram API Error:\n[{apiRequestException.ErrorCode}]\n{apiRequestException.Message}",
                _ => exception.ToString()
            };

            Console.WriteLine(ErrorMessage);
            return Task.CompletedTask;
        }

    }
}