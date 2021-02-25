using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.MultiTenancy;

namespace EasyAbp.NotificationService.Notifications
{
    public abstract class NotificationFactory<TNotificationDataModel, TCreateNotificationEto>
        where TNotificationDataModel : class
        where TCreateNotificationEto : CreateNotificationEto
    {
        public IServiceProvider ServiceProvider { get; set; }
        protected readonly object ServiceProviderLock = new ();

        protected TService LazyGetRequiredService<TService>(ref TService reference)
            => LazyGetRequiredService(typeof(TService), ref reference);

        protected TRef LazyGetRequiredService<TRef>(Type serviceType, ref TRef reference)
        {
            if (reference == null)
            {
                lock (ServiceProviderLock)
                {
                    if (reference == null)
                    {
                        reference = (TRef)ServiceProvider.GetRequiredService(serviceType);
                    }
                }
            }

            return reference;
        }
        
        protected ICurrentTenant CurrentTenant => LazyGetRequiredService(ref _currentTenant);
        private ICurrentTenant _currentTenant;

        public abstract Task<TCreateNotificationEto> CreateAsync(TNotificationDataModel model,
            IEnumerable<Guid> userIds);
        
        public virtual Task<TCreateNotificationEto> CreateAsync(TNotificationDataModel model, Guid userId)
        {
            return CreateAsync(model, new List<Guid> {userId});
        }
    }
}