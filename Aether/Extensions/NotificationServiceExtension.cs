using Aether.ExternalAccessClients;
using Aether.ExternalAccessClients.Interfaces;
using Aether.Helpers;
using Aether.Helpers.Interfaces;
using Aether.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace Aether.Extensions
{
    public static class NotificationServiceExtension
    {
        public static void HandleFOCNotificationDependencies(this IServiceCollection service, IConfiguration configuratioon, 
            string notificationSettingName,
            string notificationHelperSettingsName)
        {
            //service.Configure<NotificationServiceSettings>(configuratioon.GetSection(notificationHelperSettingsName));

            //services.Configure<ErisConfig>(Configuration.GetSection("ErisConfig"));

            service.AddSingleton<INotificationMessageHelper, NotificationMessageHelper>();
            service.AddSingleton<INotificationServiceClient, NotificationServiceClient>();
            service.AddSingleton<IFOCNotificationService, FOCNotificationService>();

        }
    }
}
