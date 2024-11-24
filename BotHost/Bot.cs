using Telegram.Bot;

namespace BotHost
{
    public class Bot
    {
        private static TelegramBotClient? client { get; set; }
        public static async Task<TelegramBotClient> GetTelegramBot()
        {
            if (client != null)
            {
                return client;
            }
            client = new TelegramBotClient("7900891678:AAFQGPBZ_BwCjx49MldyjPEKPqe-IaPZo6w");
            string hook = "marynalab4.azurewebsites.net";
            await client.SetWebhookAsync(hook);
            return client;
        }
    }
}
