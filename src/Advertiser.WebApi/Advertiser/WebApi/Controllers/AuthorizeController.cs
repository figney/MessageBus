using Advertiser.Application.Messages;
using Advertiser.WebApi.Hubs;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace Advertiser.WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthorizeController : ControllerBase
    {
        private readonly ILogger<NotifyController> _logger;
        private readonly IHubContext<MessageHuber, IMessage> _hubContext;

        public AuthorizeController(ILogger<NotifyController> logger, IHubContext<MessageHuber, IMessage> hubContext)
        {
            _logger = logger;
            _hubContext = hubContext;
        }

        public Task SigninAsync()
        {
            return Task.CompletedTask;
        }
    }
}
