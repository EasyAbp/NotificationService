using EasyAbp.NotificationService.NotificationInfos;
using JetBrains.Annotations;

namespace EasyAbp.NotificationService.Provider.PrivateMessaging;

public static class NotificationInfoExtensions
{
    public static void SetPrivateMessagingData(this NotificationInfo notificationInfo, [NotNull] string title,
        [CanBeNull] string content)
    {
        notificationInfo.SetDataValue(NotificationProviderPrivateMessagingConsts.NotificationInfoTitlePropertyName,
            title);
        notificationInfo.SetDataValue(
            NotificationProviderPrivateMessagingConsts.NotificationInfoContentPropertyName, content);
    }

    public static string GetPrivateMessagingTitle(this NotificationInfo notificationInfo)
    {
        return (string)notificationInfo.GetDataValue(NotificationProviderPrivateMessagingConsts
            .NotificationInfoTitlePropertyName);
    }

    public static string GetPrivateMessagingContent(this NotificationInfo notificationInfo)
    {
        return (string)notificationInfo.GetDataValue(NotificationProviderPrivateMessagingConsts
            .NotificationInfoContentPropertyName);
    }
}