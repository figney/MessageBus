using Advertiser.Application.Messages;
using Advertiser.Application.Onlines;
using Advertiser.WebApi.Hubs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace Advertiser.WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class NotifyController : ControllerBase
    {
        private readonly ILogger<NotifyController> _logger;
        private readonly IOnliner _onliner;
        private readonly IHubContext<MessageHuber, IMessage> _hubContext;

        public NotifyController(ILogger<NotifyController> logger, IOnliner onliner, IHubContext<MessageHuber, IMessage> hubContext)
        {
            _logger = logger;
            _onliner = onliner;
            _hubContext = hubContext;
        }

        [HttpPut]
        [Route("all")]
        public async Task NotifyAllAsync([FromBody] object message)
        {
            _logger.LogInformation(message.ToString());
            await _hubContext.Clients.All.NotifyAsync(message.ToString()!);
        }

        [HttpPut]
        [Route("user/{id}")]
        public async Task NotifyUserAsync(long id, [FromBody] object message)
        {
            _logger.LogInformation(message.ToString());
            if (_onliner.Users.TryGetValue(id, out string? connectionId))
            {
                _logger.LogInformation("SendTo: {id}  {connectionId}", id, connectionId);
                await _hubContext.Clients.Client(connectionId).NotifyAsync(message.ToString()!);
            }
        }
    }
}
