using System;
using Volo.Abp.Domain.Repositories;

namespace EasyAbp.NotificationService.NotificationInfos
{
    public interface INotificationInfoRepository : IRepository<NotificationInfo, Guid>
    {
    }
}