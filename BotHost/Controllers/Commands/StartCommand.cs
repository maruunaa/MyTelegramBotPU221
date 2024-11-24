using Telegram.Bot.Types;
using Telegram.Bot;

namespace BotHost.Controllers.Commands
{
    public class StartCommand : ICommand
    {
        public TelegramBotClient Client => Bot.GetTelegramBot().Result;

        public string Name => "/start";
        public async Task Execute(Update update)
        {
            long chatId = update.Message.Chat.Id;
            string commands = @"Список команд: 
					    /start - запуск бота
					    /inline - меню
					    /keyboard - вивід клавіатури";

            await Client.SendTextMessageAsync(
                chatId,
                text: commands);
        }
    }
}
