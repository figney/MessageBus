using Advertiser.Application.DomainServices;
using Advertiser.Domain.Notifications;
using Microsoft.Extensions.Hosting;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Advertiser.BackgroundServices
{
    public class WatchingNotificationBackgroundService : BackgroundService
    {
        private readonly NotificationDomainService _notificationDomainService;

        public WatchingNotificationBackgroundService(NotificationDomainService notificationDomainService)
        {
            _notificationDomainService = notificationDomainService;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                try
                {
                    var documents = await _notificationDomainService.GetNotificationsAsync(1);
                    foreach (var document in documents)
                    {
                        Console.WriteLine(document.Id);
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
        }
    }
}
