using EasyAbp.NotificationService.Options;
using Microsoft.Extensions.Options;
using Volo.Abp.DependencyInjection;

namespace EasyAbp.NotificationService.Notifications;

public class NotificationManagerResolver : INotificationManagerResolver, ITransientDependency
{
    private readonly IAbpLazyServiceProvider _lazyServiceProvider;
    private readonly NotificationServiceOptions _options;

    public NotificationManagerResolver(
        IAbpLazyServiceProvider lazyServiceProvider,
        IOptions<NotificationServiceOptions> options)
    {
        _lazyServiceProvider = lazyServiceProvider;
        _options = options.Value;
    }

    public virtual INotificationManager Resolve(string notificationMethod)
    {
        return (INotificationManager)_lazyServiceProvider.LazyGetRequiredService(_options.Providers[notificationMethod]
            .NotificationManagerType);
    }
}