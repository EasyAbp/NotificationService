using System.Threading.Tasks;
using EasyAbp.NotificationService.Notifications;
using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.BackgroundJobs;
using Volo.Abp.Domain.Entities.Events;
using Volo.Abp.Uow;

namespace EasyAbp.NotificationService.Provider.Sms;

public class SmsNotificationCreationEventHandler : NotificationCreationEventHandlerBase
{
    private readonly IUnitOfWorkManager _unitOfWorkManager;
    private readonly IServiceScopeFactory _serviceScopeFactory;

    public SmsNotificationCreationEventHandler(
        IUnitOfWorkManager unitOfWorkManager,
        IServiceScopeFactory serviceScopeFactory)
    {
        _unitOfWorkManager = unitOfWorkManager;
        _serviceScopeFactory = serviceScopeFactory;
    }

    protected override string NotificationMethod => NotificationProviderSmsConsts.NotificationMethod;

    protected override Task InternalHandleEventAsync(EntityCreatedEventData<Notification> eventData)
    {
        // todo: should use Stepping.NET or distributed event bus to ensure done?
        _unitOfWorkManager.Current.OnCompleted(async () =>
        {
            using var scope = _serviceScopeFactory.CreateScope();

            var backgroundJobManager = scope.ServiceProvider.GetRequiredService<IBackgroundJobManager>();

            await backgroundJobManager.EnqueueAsync(
                new SmsNotificationSendingJobArgs(eventData.Entity.TenantId, eventData.Entity.Id));
        });

        return Task.CompletedTask;
    }
}