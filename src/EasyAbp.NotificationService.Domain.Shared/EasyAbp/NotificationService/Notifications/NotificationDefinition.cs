using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EasyAbp.NotificationService.Notifications
{
    public abstract class NotificationDefinition<TCreateNotificationEto> where TCreateNotificationEto : CreateNotificationEto
    {
        public abstract Task<TCreateNotificationEto> CreateAsync(IEnumerable<Guid> userIds);
    }
}