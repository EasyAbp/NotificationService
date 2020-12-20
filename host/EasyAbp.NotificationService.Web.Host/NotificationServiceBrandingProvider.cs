using Volo.Abp.AspNetCore.Mvc.UI.Theme.Shared.Components;
using Volo.Abp.DependencyInjection;

namespace EasyAbp.NotificationService
{
    [Dependency(ReplaceServices = true)]
    public class NotificationServiceBrandingProvider : DefaultBrandingProvider
    {
        public override string AppName => "NotificationService";
    }
}
