using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EasyAbp.NotificationService.Notifications
{
    public abstract class NotificationFactory<TNotificationDataModel, TCreateNotificationEto>
        where TNotificationDataModel : class
        where TCreateNotificationEto : CreateNotificationEto
    {
        public abstract Task<TCreateNotificationEto> CreateAsync(TNotificationDataModel model,
            IEnumerable<Guid> userIds);
        
        public virtual Task<TCreateNotificationEto> CreateAsync(TNotificationDataModel model, Guid userId)
        {
            return CreateAsync(model, new List<Guid> {userId});
        }
    }
}