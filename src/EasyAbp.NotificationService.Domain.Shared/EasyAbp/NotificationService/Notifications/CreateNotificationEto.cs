using System;
using System.Collections.Generic;
using Volo.Abp.MultiTenancy;

namespace EasyAbp.NotificationService.Notifications
{
    [Serializable]
    public abstract class CreateNotificationEto : IMultiTenant
    {
        public Guid? TenantId { get; }

        public IEnumerable<Guid> UserIds { get; set; }

        public CreateNotificationEto(Guid? tenantId, IEnumerable<Guid> userIds)
        {
            TenantId = tenantId;
            UserIds = userIds;
        }

        public CreateNotificationEto(Guid? tenantId, Guid userId)
        {
            TenantId = tenantId;
            UserIds = new List<Guid> {userId};
        }
    }
}