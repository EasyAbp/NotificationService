using EasyAbp.NotificationService.NotificationInfos;
using JetBrains.Annotations;

namespace EasyAbp.NotificationService.Provider.PrivateMessaging
{
    public static class NotificationInfoExtensions
    {
        public static void SetPrivateMessagingData(this NotificationInfo notificationInfo, [NotNull] string title,
            [CanBeNull] string content)
        {
            notificationInfo.SetDataValue(NotificationProviderPrivateMessagingConsts.NotificationInfoTitlePropertyName, title);
            notificationInfo.SetDataValue(NotificationProviderPrivateMessagingConsts.NotificationInfoContentPropertyName, content);
        }
    }
}