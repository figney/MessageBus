using Advertiser.Application.DomainServices;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Advertiser.BackgroundServices
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
                    await _userAdvertiseDomainService.RemoveExpiredAdvertiseOlderThanAsync(-30);
                    await Task.Delay(1000 * 60, stoppingToken);
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "RemovingUserAdvertiseBackgroundService: {0}", ex.Message);
                }
            }
        }
    }
}
