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
            IEnumerable<Guid> userIds,
            [NotNull] string subject,
            [CanBeNull] string body) : base(userIds)
        {
            Subject = subject;
            Body = body;
        }
        
        public CreateEmailNotificationEto(
            Guid userId,
            [NotNull] string subject,
            [CanBeNull] string body) : base(userId)
        {
            Subject = subject;
            Body = body;
        }
    }
}