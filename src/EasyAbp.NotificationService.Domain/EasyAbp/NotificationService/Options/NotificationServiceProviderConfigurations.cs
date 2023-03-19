using System.Collections.Generic;

namespace EasyAbp.NotificationService.Options;

public class NotificationServiceProviderConfigurations : Dictionary<string, NotificationServiceProviderConfiguration>
{
    public void AddProvider(NotificationServiceProviderConfiguration providerConfiguration)
    {
        this[providerConfiguration.NotificationMethod] = providerConfiguration;
    }
}