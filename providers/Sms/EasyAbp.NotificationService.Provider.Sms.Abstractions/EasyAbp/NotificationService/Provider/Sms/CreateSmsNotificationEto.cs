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
        public string Text { get; set; }

        [NotNull]
        public IDictionary<string, object> Properties { get; set; }
        
        public CreateSmsNotificationEto(
            Guid? tenantId,
            IEnumerable<Guid> userIds,
            [NotNull] string text,
            [NotNull] IDictionary<string, object> properties) : base(tenantId, userIds)
        {
            Text = text;
            Properties = properties;
        }
        
        public CreateSmsNotificationEto(
            Guid? tenantId,
            Guid userId,
            [NotNull] string text,
            [NotNull] IDictionary<string, object> properties) : base(tenantId, userId)
        {
            Text = text;
            Properties = properties;
        }
    }
}