using Volo.Abp.DependencyInjection;
using Volo.Abp.Ui.Branding;

namespace EasyAbp.NotificationService
{
    [Dependency(ReplaceServices = true)]
    public class NotificationServiceBrandingProvider : DefaultBrandingProvider
    {
        public override string AppName => "NotificationService";
    }
}
