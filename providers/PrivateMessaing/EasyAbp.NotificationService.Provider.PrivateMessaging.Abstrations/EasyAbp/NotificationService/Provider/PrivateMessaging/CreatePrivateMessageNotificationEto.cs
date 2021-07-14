using System;
using System.Collections.Generic;
using EasyAbp.NotificationService.Notifications;
using JetBrains.Annotations;

namespace EasyAbp.NotificationService.Provider.PrivateMessaging
{
    [Serializable]
    public class CreatePrivateMessageNotificationEto : CreateNotificationEto
    {
        [NotNull]
        public string Title { get; set; }

        [CanBeNull]
        public string Content { get; set; }


        public CreatePrivateMessageNotificationEto(
            Guid? tenantId,
            IEnumerable<Guid> userIds,
            [NotNull] string title,
            [CanBeNull] string content) : base(tenantId, userIds)
        {
            Title = title;
            Content = content;
        }
        
        public CreatePrivateMessageNotificationEto(
            Guid? tenantId,
            Guid userId,
            [NotNull] string title,
            [CanBeNull] string content) : base(tenantId, userId)
        {
            Title = title;
            Content = content;
        }
    }
}