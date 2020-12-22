using System;
using System.Collections.Generic;
using EasyAbp.NotificationService.Notifications;
using JetBrains.Annotations;

namespace EasyAbp.NotificationService.Provider.Sms
{
    [Serializable]
    public class CreateSmsNotificationEto : CreateNotificationEto
    {
        [NotNull]
        public string Text { get; protected set; }

        [NotNull]
        public IDictionary<string, object> Properties { get; protected set; }

        public CreateSmsNotificationEto(
            IEnumerable<Guid> userIds,
            [NotNull] string text,
            [NotNull] IDictionary<string, object> properties) : base(userIds)
        {
            Text = text;
            Properties = properties;
        }
        
        public CreateSmsNotificationEto(
            Guid userId,
            [NotNull] string text,
            [NotNull] IDictionary<string, object> properties) : base(userId)
        {
            Text = text;
            Properties = properties;
        }
    }
}