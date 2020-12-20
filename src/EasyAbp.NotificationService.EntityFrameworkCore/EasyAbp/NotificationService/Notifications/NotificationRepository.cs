using System;
using EasyAbp.NotificationService.EntityFrameworkCore;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace EasyAbp.NotificationService.Notifications
{
    public class NotificationRepository : EfCoreRepository<INotificationServiceDbContext, Notification, Guid>, INotificationRepository
    {
        public NotificationRepository(IDbContextProvider<INotificationServiceDbContext> dbContextProvider) : base(dbContextProvider)
        {
        }
    }
}