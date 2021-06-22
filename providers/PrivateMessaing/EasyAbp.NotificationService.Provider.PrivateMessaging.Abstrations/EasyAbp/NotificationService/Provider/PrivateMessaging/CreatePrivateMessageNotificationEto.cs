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
        public string Title { get; protected set; }

        [CanBeNull]
        public string Content { get; protected set; }

        [NotNull]
        public IDictionary<string, object> Properties { get; protected set; }

        protected CreatePrivateMessageNotificationEto()
        {
        }
        
        public CreatePrivateMessageNotificationEto(
            Guid? tenantId,
            IEnumerable<Guid> userIds,
            [NotNull] string title,
            [CanBeNull] string content,
            [NotNull] IDictionary<string, object> properties) : base(tenantId, userIds)
        {
            Title = title;
            Content = content;
            Properties = properties;
        }
        
        public CreatePrivateMessageNotificationEto(
            Guid? tenantId,
            Guid userId,
            [NotNull] string title,
            [CanBeNull] string content,
            [NotNull] IDictionary<string, object> properties) : base(tenantId, userId)
        {
            Title = title;
            Content = content;
            Properties = properties;
        }
    }
}