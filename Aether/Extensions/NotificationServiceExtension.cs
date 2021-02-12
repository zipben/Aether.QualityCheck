using Aether.ExternalAccessClients;
using Aether.ExternalAccessClients.Interfaces;
using Aether.Helpers;
using Aether.Helpers.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace Aether.Extensions
{
    public static class NotificationServiceExtension
    {
        public static void UseFOCNotificationService(this IServiceCollection service)
        {
            service.AddSingleton<INotificationMessageHelper, NotificationMessageHelper>();
            service.AddSingleton<INotificationServiceClient, NotificationServiceClient>();
            service.AddSingleton<IFOCNotificationService, FOCNotificationService>();
        }
    }
}
