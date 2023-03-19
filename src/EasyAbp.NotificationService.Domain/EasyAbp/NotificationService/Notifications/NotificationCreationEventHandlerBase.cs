using System.Threading.Tasks;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Domain.Entities.Events;
using Volo.Abp.EventBus;

namespace EasyAbp.NotificationService.Notifications;

public abstract class NotificationCreationEventHandlerBase : ILocalEventHandler<EntityCreatedEventData<Notification>>,
    ITransientDependency
{
    protected abstract string NotificationMethod { get; }

    public virtual async Task HandleEventAsync(EntityCreatedEventData<Notification> eventData)
    {
        if (NotificationMethod != eventData.Entity.NotificationMethod || eventData.Entity.CompletionTime.HasValue)
        {
            return;
        }

        await InternalHandleEventAsync(eventData);
    }

    protected abstract Task InternalHandleEventAsync(EntityCreatedEventData<Notification> eventData);
}