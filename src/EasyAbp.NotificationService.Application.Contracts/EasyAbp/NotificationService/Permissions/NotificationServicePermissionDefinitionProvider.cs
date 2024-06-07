using EasyAbp.NotificationService.Localization;
using Volo.Abp.Authorization.Permissions;
using Volo.Abp.Localization;

namespace EasyAbp.NotificationService.Permissions
{
    public class NotificationServicePermissionDefinitionProvider : PermissionDefinitionProvider
    {
        public override void Define(IPermissionDefinitionContext context)
        {
            var myGroup = context.AddGroup(NotificationServicePermissions.GroupName, L("Permission:NotificationService"));

            var notificationPermission = myGroup.AddPermission(NotificationServicePermissions.Notification.Default, L("Permission:Notification"));
            notificationPermission.AddChild(NotificationServicePermissions.Notification.Manage, L("Permission:Manage"));
        }

        private static LocalizableString L(string name)
        {
            return LocalizableString.Create<NotificationServiceResource>(name);
        }
    }
}
