using System.Threading.Tasks;

namespace Advertiser.Application.Messages
{
    public interface IMessage
    {
        Task ConnectAsync(string message);

        Task DisconnectAsync();

        Task NotifyAsync(string message);
    }
}
