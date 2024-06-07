using System;
using EasyAbp.NotificationService.Permissions;
using EasyAbp.NotificationService.NotificationInfos.Dtos;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.ObjectExtending;

namespace EasyAbp.NotificationService.NotificationInfos
{
    public class NotificationInfoAppService : ReadOnlyAppService<NotificationInfo, NotificationInfoDto, Guid,
        PagedAndSortedResultRequestDto>, INotificationInfoAppService
    {
        protected override string GetPolicyName { get; set; } = NotificationServicePermissions.Notification.Manage;
        protected override string GetListPolicyName { get; set; } = NotificationServicePermissions.Notification.Manage;

        private readonly INotificationInfoRepository _repository;

        public NotificationInfoAppService(INotificationInfoRepository repository) : base(repository)
        {
            _repository = repository;
        }

        protected override NotificationInfoDto MapToGetOutputDto(NotificationInfo entity)
        {
            var dto = ObjectMapper.Map<NotificationInfo, NotificationInfoDto>(entity);

            entity.MapExtraPropertiesTo(dto, MappingPropertyDefinitionChecks.None);

            return dto;
        }
    }
}