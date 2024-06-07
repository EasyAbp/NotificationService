using System;
using Volo.Abp.Application.Dtos;

namespace EasyAbp.NotificationService.NotificationInfos.Dtos
{
    [Serializable]
    public class NotificationInfoDto : ExtensibleFullAuditedEntityDto<Guid>
    {
    }
}