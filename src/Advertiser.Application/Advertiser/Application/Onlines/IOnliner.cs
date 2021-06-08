using System.Collections.Generic;
using System.Threading.Tasks;

namespace Advertiser.Application.Onlines
{
    public interface IOnliner
    {
        int Count { get; }

        Dictionary<long, string> Users { get; }

        Task OnConnectedAsync(string id, long userId);

        Task OnDisconnectedAsync(string id);
    }
}
