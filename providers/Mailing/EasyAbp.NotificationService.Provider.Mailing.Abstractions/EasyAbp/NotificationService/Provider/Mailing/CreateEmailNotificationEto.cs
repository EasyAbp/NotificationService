using System;
using System.Collections.Generic;
using EasyAbp.NotificationService.Notifications;
using JetBrains.Annotations;

namespace EasyAbp.NotificationService.Provider.Mailing
{
    [Serializable]
    public class CreateEmailNotificationEto : CreateNotificationEto
    {
        [NotNull]
        public string Subject { get; set; }
        
        [CanBeNull]
        public string Body { get; set; }

        
        public CreateEmailNotificationEto(
            Guid? tenantId,
            IEnumerable<Guid> userIds,
            [NotNull] string subject,
            [CanBeNull] string body) : base(tenantId, userIds)
        {
            Subject = subject;
            Body = body;
        }
        
        public CreateEmailNotificationEto(
            Guid? tenantId,
            Guid userId,
            [NotNull] string subject,
            [CanBeNull] string body) : base(tenantId, userId)
        {
            Subject = subject;
            Body = body;
        }
    }
}