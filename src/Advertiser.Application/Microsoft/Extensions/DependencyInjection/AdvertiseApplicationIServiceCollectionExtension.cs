using Advertiser.Application.DomainServices;
using Advertiser.Application.Onlines;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class AdvertiseApplicationIServiceCollectionExtension
    {
        public static IServiceCollection AddAdvertiseApplication(this IServiceCollection services)
        {
            services.AddSingleton<UserAdvertiseDomainService>();
            services.AddTransient<NotificationDomainService>();
            services.AddSingleton<IOnliner, Onliner>();
            return services;
        }
    }
}
