using System;
using EasyAbp.NotificationService.EntityFrameworkCore;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace EasyAbp.NotificationService.NotificationInfos
{
    public class NotificationInfoRepository : EfCoreRepository<INotificationServiceDbContext, NotificationInfo, Guid>, INotificationInfoRepository
    {
        public NotificationInfoRepository(IDbContextProvider<INotificationServiceDbContext> dbContextProvider) : base(dbContextProvider)
        {
        }
    }
}