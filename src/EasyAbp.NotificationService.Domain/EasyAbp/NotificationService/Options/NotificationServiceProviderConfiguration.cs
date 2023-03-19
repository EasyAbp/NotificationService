using System;

namespace EasyAbp.NotificationService.Options;

public class NotificationServiceProviderConfiguration
{
    public string NotificationMethod { get; set; }

    public Type NotificationManagerType { get; set; }

    protected NotificationServiceProviderConfiguration()
    {
    }

    public NotificationServiceProviderConfiguration(string notificationMethod, Type notificationManagerType)
    {
        NotificationMethod = notificationMethod;
        NotificationManagerType = notificationManagerType;
    }
}