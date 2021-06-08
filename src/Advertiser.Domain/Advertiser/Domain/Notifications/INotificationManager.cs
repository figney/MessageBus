using System.Threading.Tasks;

namespace Advertiser.Domain.Notifications
{
    public interface INotificationManager
    {
        Task RemoveUselessNoticesAsync();
    }
}
