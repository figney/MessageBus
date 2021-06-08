using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Advertiser.Application.Onlines
{
    public class Onliner : IOnliner
    {
        private readonly ILogger<Onliner> _logger;

        private Dictionary<long, string> _users = new();

        public Onliner(ILogger<Onliner> logger)
        {
            _logger = logger;
        }

        public int Count => _users.Count;

        public Dictionary<long, string> Users => _users;

        public Task OnConnectedAsync(string id, long userId)
        {
            _users.TryAdd(userId, id);
            _logger.LogInformation("OnConnected: {id} <===> {userId}", id, userId);
            return Task.CompletedTask;
        }

        public Task OnDisconnectedAsync(string id)
        {
            _users.Remove(1);
            _logger.LogInformation("OnDisconnected: {id}", id);
            return Task.CompletedTask;
        }
    }
}
