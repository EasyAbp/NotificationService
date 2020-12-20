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
            notificationPermission.AddChild(NotificationServicePermissions.Notification.Create, L("Permission:Create"));
            notificationPermission.AddChild(NotificationServicePermissions.Notification.Update, L("Permission:Update"));
            notificationPermission.AddChild(NotificationServicePermissions.Notification.Delete, L("Permission:Delete"));
        }

        private static LocalizableString L(string name)
        {
            return LocalizableString.Create<NotificationServiceResource>(name);
        }
    }
}
