using System;
using EasyAbp.NotificationService.Permissions;
using EasyAbp.NotificationService.Notifications.Dtos;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace EasyAbp.NotificationService.Notifications
{
    public class NotificationAppService : CrudAppService<Notification, NotificationDto, Guid, PagedAndSortedResultRequestDto, CreateUpdateNotificationDto, CreateUpdateNotificationDto>,
        INotificationAppService
    {
        protected override string GetPolicyName { get; set; } = NotificationServicePermissions.Notification.Default;
        protected override string GetListPolicyName { get; set; } = NotificationServicePermissions.Notification.Default;
        protected override string CreatePolicyName { get; set; } = NotificationServicePermissions.Notification.Create;
        protected override string UpdatePolicyName { get; set; } = NotificationServicePermissions.Notification.Update;
        protected override string DeletePolicyName { get; set; } = NotificationServicePermissions.Notification.Delete;

        private readonly INotificationRepository _repository;
        
        public NotificationAppService(INotificationRepository repository) : base(repository)
        {
            _repository = repository;
        }
    }
}
