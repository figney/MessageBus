using Advertiser.Application.DomainServices;
using Advertiser.Application.Messages;
using Advertiser.Application.Onlines;
using Advertiser.Domain.Notifications;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Advertiser.WebApi.Hubs
{
    public class MessageHuber : Hub<IMessage>
    {
        private readonly IOnliner _onliner;
        private readonly ILogger<MessageHuber> _logger;

        public MessageHuber(IOnliner onliner, ILogger<MessageHuber> logger)
        {
            _onliner = onliner;
            _logger = logger;
        }

        public override async Task OnConnectedAsync()
        {
            _logger.LogInformation($"Client:{ Context.ConnectionId } Connected");
            await Clients.Caller.ConnectAsync("Welcome");
            await base.OnConnectedAsync();
        }

        public override async Task OnDisconnectedAsync(Exception? exception)
        {
            _logger.LogInformation($"Client:{ Context.ConnectionId } Disconnected");
            await _onliner.OnDisconnectedAsync(Context.ConnectionId!);
            await base.OnDisconnectedAsync(exception);
        }

        public async Task SigninAsync(long userId)
        {
            await _onliner.OnConnectedAsync(Context.ConnectionId, userId);
        }
    }
}
