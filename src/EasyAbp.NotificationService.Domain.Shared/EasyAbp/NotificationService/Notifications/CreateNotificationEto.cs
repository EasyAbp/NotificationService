using System;
using System.Collections.Generic;

namespace EasyAbp.NotificationService.Notifications
{
    [Serializable]
    public abstract class CreateNotificationEto
    {
        public IEnumerable<Guid> UserIds { get; set; }

        public CreateNotificationEto(IEnumerable<Guid> userIds)
        {
            UserIds = userIds;
        }

        public CreateNotificationEto(Guid userId)
        {
            UserIds = new List<Guid> {userId};
        }
    }
}