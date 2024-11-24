using Telegram.Bot.Types;

namespace BotHost.Controllers
{
    public interface ITelegramUpdateListener
    {
        Task GetUpdate(Update update);
    }
}
