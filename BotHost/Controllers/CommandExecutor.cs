using BotHost.Controllers.Commands;
using System.Windows.Input;
using Telegram.Bot.Types;
using ICommand = BotHost.Controllers.Commands.ICommand;


namespace BotHost.Controllers
{
    public class CommandExecutor : ITelegramUpdateListener
    {
        private List<ICommand> commands;
        public CommandExecutor()
        {
            commands = new List<ICommand>()
            {
                new StartCommand(),
                new InlineCommand(),
                new KeyboardCommand()
            };
        }
        public async Task GetUpdate(Update update)
        {
            Message msg = update.Message;
            ICommand searchCommand = commands.FirstOrDefault(command => command.Name == msg.Text);
            if (searchCommand != null)
            {
                await searchCommand.Execute(update);
            }
            else
                await new NotFoundCommand().Execute(update);
        }
    }
}
