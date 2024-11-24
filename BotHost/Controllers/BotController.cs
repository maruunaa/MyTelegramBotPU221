using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Telegram.Bot.Types;

namespace BotHost.Controllers
{
    [Route("/")]
    [ApiController]
    public class BotController : ControllerBase
    {
        
        private UpdateDistributor<CommandExecutor> updateDistributor = new UpdateDistributor<CommandExecutor>();

        [HttpPost]
        public async void Post([FromBody] Update update)
        {
            if (update.Message == null)
            {
                return;
            }
            await updateDistributor.GetUpdate(update);


        }
        [HttpGet]

        public string Get()
        {
            return "Telegram bot was started";
        }
    }
}
