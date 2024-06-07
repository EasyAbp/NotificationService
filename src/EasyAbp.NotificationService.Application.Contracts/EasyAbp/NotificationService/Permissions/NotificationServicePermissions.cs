using Volo.Abp.Reflection;

namespace EasyAbp.NotificationService.Permissions
{
    public class NotificationServicePermissions
    {
        public const string GroupName = "EasyAbp.NotificationService";

        public static string[] GetAll()
        {
            return ReflectionHelper.GetPublicConstantsRecursively(typeof(NotificationServicePermissions));
        }

        public class Notification
        {
            public const string Default = GroupName + ".Notification";
            public const string Manage = Default + ".Manage";
        }
    }
}
