using Telegram.Bot.Types.ReplyMarkups;
using Telegram.Bot.Types;
using Telegram.Bot;

namespace BotHost.Controllers.Commands
{
    public class InlineCommand : ICommand
    {
        public TelegramBotClient Client => Bot.GetTelegramBot().Result;

        public string Name => "/inline";


        public async Task Execute(Update update)
        {
            long chatId = update.Message.Chat.Id;


            var inlineKeyboard = new InlineKeyboardMarkup(new[] {
                new [] {
                    InlineKeyboardButton.WithUrl("Applied Mathematics", "https://lpnu.ua/en/am"),
                    InlineKeyboardButton.WithUrl("insta of Department", "https://www.instagram.com/applied_math_lp?igsh=Mzlia3BoeXMzNmRy")
            },
                new [] {
                    InlineKeyboardButton.WithUrl("Teachers", "https://amath.lp.edu.ua/staff/teachers"),
                    InlineKeyboardButton.WithUrl("News", "https://amath.lp.edu.ua/posts"),
            },
         });

            await Client.SendTextMessageAsync(
            chatId: chatId,
            text: "Виберіть пункт меню: ",
                replyMarkup: inlineKeyboard
                );
        }



    }
}
