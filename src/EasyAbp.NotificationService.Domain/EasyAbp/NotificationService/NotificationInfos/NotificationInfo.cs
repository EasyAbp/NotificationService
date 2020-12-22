using System;
using System.Collections.Generic;
using JetBrains.Annotations;
using Volo.Abp.Data;
using Volo.Abp.Domain.Entities.Auditing;
using Volo.Abp.MultiTenancy;

namespace EasyAbp.NotificationService.NotificationInfos
{
    public class NotificationInfo : FullAuditedAggregateRoot<Guid>, IMultiTenant
    {
        public virtual Guid? TenantId { get; protected set; }

        protected NotificationInfo()
        {
        }

        public NotificationInfo(
            Guid id,
            Guid? tenantId) : base(id)
        {
            TenantId = tenantId;
        }
        
        public NotificationInfo(
            Guid id,
            Guid? tenantId,
            Dictionary<string, string> dataDictionary
        ) : base(id)
        {
            TenantId = tenantId;

            foreach (var pair in dataDictionary)
            {
                SetDataValue(pair.Key, pair.Value);
            }
        }
        
        public object GetDataValue([NotNull] string name)
        {
            return this.GetProperty(name);
        }

        public void SetDataValue([NotNull] string name, [CanBeNull] object value)
        {
            this.SetProperty(name, value);
        }
    }
}
