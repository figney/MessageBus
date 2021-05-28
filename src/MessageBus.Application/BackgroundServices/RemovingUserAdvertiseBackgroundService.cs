using MessageBus.Application.DomainServices;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace MessageBus.Application.BackgroundServices
{
    public class RemovingUserAdvertiseBackgroundService : BackgroundService
    {
        private readonly UserAdvertiseDomainService _userAdvertiseDomainService;
        private readonly ILogger<RemovingUserAdvertiseBackgroundService> _logger;

        public RemovingUserAdvertiseBackgroundService(UserAdvertiseDomainService userAdvertiseDomainService, ILogger<RemovingUserAdvertiseBackgroundService> logger)
        {
            _userAdvertiseDomainService = userAdvertiseDomainService;
            _logger = logger;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                try
                {
                    var ids = await _userAdvertiseDomainService.FindAdvertiseIdsAsync(DateTime.Now.AddDays(-30), "InProgress", 1000);
                    if (ids.Count == 0)
                    {
                        await Task.Delay(1000 * 60, stoppingToken);
                        continue;
                    }
                    await _userAdvertiseDomainService.UpdateAdvertiseToExpiredAsync(ids.ToArray());
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "RemovingUserAdvertiseBackgroundService: {0}", ex.Message);
                }
            }
        }
    }
}
