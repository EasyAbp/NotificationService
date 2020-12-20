using System;
using EasyAbp.NotificationService.Permissions;
using EasyAbp.NotificationService.NotificationInfos.Dtos;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace EasyAbp.NotificationService.NotificationInfos
{
    public class NotificationInfoAppService : CrudAppService<NotificationInfo, NotificationInfoDto, Guid, PagedAndSortedResultRequestDto, CreateUpdateNotificationInfoDto, CreateUpdateNotificationInfoDto>,
        INotificationInfoAppService
    {
        protected override string GetPolicyName { get; set; } = NotificationServicePermissions.Notification.Default;
        protected override string GetListPolicyName { get; set; } = NotificationServicePermissions.Notification.Default;
        protected override string CreatePolicyName { get; set; } = NotificationServicePermissions.Notification.Create;
        protected override string UpdatePolicyName { get; set; } = NotificationServicePermissions.Notification.Update;
        protected override string DeletePolicyName { get; set; } = NotificationServicePermissions.Notification.Delete;

        private readonly INotificationInfoRepository _repository;
        
        public NotificationInfoAppService(INotificationInfoRepository repository) : base(repository)
        {
            _repository = repository;
        }
    }
}
