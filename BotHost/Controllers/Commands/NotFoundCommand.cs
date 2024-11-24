using Telegram.Bot.Types;
using Telegram.Bot;

namespace BotHost.Controllers.Commands
{
    public class NotFoundCommand : ICommand
    {
        public TelegramBotClient Client => Bot.GetTelegramBot().Result;

        public string Name => "";

        public async Task Execute(Update update)
        {
            long chatId = update.Message.Chat.Id;

            string text = "Не знаю такої команди";

            await Client.SendTextMessageAsync(
                chatId,
                text: text);
        }

    }
}
