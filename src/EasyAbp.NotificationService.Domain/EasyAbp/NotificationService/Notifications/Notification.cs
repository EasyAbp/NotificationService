using System;
using JetBrains.Annotations;
using Volo.Abp.Domain.Entities.Auditing;
using Volo.Abp.MultiTenancy;
using Volo.Abp.Timing;

namespace EasyAbp.NotificationService.Notifications
{
    public class Notification : CreationAuditedAggregateRoot<Guid>, INotification, IMultiTenant
    {
        public virtual Guid? TenantId { get; protected set; }

        public virtual Guid UserId { get; protected set; }

        public virtual string UserName { get; protected set; }

        public virtual Guid NotificationInfoId { get; protected set; }

        [NotNull]
        public virtual string NotificationMethod { get; protected set; }

        public virtual bool? Success { get; protected set; }

        public virtual DateTime? CompletionTime { get; protected set; }

        [CanBeNull]
        public virtual string FailureReason { get; protected set; }

        public virtual Guid? RetryForNotificationId { get; protected set; }

        protected Notification()
        {
        }

        public Notification(
            Guid id,
            Guid? tenantId,
            Guid userId,
            string userName,
            Guid notificationInfoId,
            [NotNull] string notificationMethod
        ) : base(id)
        {
            TenantId = tenantId;
            UserId = userId;
            UserName = userName;
            NotificationInfoId = notificationInfoId;
            NotificationMethod = notificationMethod;
        }

        public void SetResult(IClock clock, bool success, [CanBeNull] string failureReason = null)
        {
            if (CompletionTime.HasValue)
            {
                throw new NotificationHasBeenCompletedException();
            }

            CompletionTime = clock.Now;
            Success = success;
            FailureReason = failureReason;
        }
    }
}