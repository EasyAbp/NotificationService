using System;
using EasyAbp.NotificationService.NotificationInfos.Dtos;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace EasyAbp.NotificationService.NotificationInfos
{
    public interface INotificationInfoAppService :
        IReadOnlyAppService<
            NotificationInfoDto,
            Guid,
            PagedAndSortedResultRequestDto>
    {
    }
}