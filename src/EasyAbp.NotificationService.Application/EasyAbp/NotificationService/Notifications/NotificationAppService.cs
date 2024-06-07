using System;
using System.Linq;
using System.Threading.Tasks;
using EasyAbp.NotificationService.Permissions;
using EasyAbp.NotificationService.Notifications.Dtos;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace EasyAbp.NotificationService.Notifications
{
    public class NotificationAppService :
        ReadOnlyAppService<Notification, NotificationDto, Guid, NotificationGetListInput>,
        INotificationAppService
    {
        protected override string GetPolicyName { get; set; } = NotificationServicePermissions.Notification.Manage;
        protected override string GetListPolicyName { get; set; } = NotificationServicePermissions.Notification.Manage;

        private readonly INotificationRepository _repository;

        public NotificationAppService(INotificationRepository repository) : base(repository)
        {
            _repository = repository;
        }

        protected override async Task<IQueryable<Notification>> CreateFilteredQueryAsync(
            NotificationGetListInput input)
        {
            return (await base.CreateFilteredQueryAsync(input))
                .WhereIf(input.UserId != null, x => x.UserId == input.UserId)
                .WhereIf(!input.UserName.IsNullOrWhiteSpace(), x => x.UserName.Contains(input.UserName!))
                .WhereIf(input.NotificationInfoId != null, x => x.NotificationInfoId == input.NotificationInfoId)
                .WhereIf(!input.NotificationMethod.IsNullOrWhiteSpace(),
                    x => x.NotificationMethod.Contains(input.NotificationMethod!))
                .WhereIf(input.Success != null, x => x.Success == input.Success)
                .WhereIf(input.CreationTime != null, x => x.CreationTime == input.CreationTime)
                .WhereIf(input.CreatorId != null, x => x.CreatorId == input.CreatorId)
                .WhereIf(input.CompletionTime != null, x => x.CompletionTime == input.CompletionTime)
                .WhereIf(!input.FailureReason.IsNullOrWhiteSpace(), x => x.FailureReason.Contains(input.FailureReason!))
                .WhereIf(input.RetryForNotificationId != null,
                    x => x.RetryForNotificationId == input.RetryForNotificationId)
                ;
        }
    }
}