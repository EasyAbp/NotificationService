using System;
using EasyAbp.NotificationService.Notifications.Dtos;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace EasyAbp.NotificationService.Notifications
{
    public interface INotificationAppService :
        ICrudAppService< 
            NotificationDto, 
            Guid, 
            PagedAndSortedResultRequestDto,
            CreateUpdateNotificationDto,
            CreateUpdateNotificationDto>
    {

    }
}