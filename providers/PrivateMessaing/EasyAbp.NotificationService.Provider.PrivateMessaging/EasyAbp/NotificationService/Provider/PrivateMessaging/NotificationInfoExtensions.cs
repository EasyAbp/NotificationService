using EasyAbp.NotificationService.NotificationInfos;
using JetBrains.Annotations;

namespace EasyAbp.NotificationService.Provider.PrivateMessaging;

public static class NotificationInfoExtensions
{
    public static void SetPrivateMessagingData(this NotificationInfo notificationInfo, [NotNull] string title,
        [CanBeNull] string content, bool sendFromCreator)
    {
        notificationInfo.SetDataValue(NotificationProviderPrivateMessagingConsts.NotificationInfoTitlePropertyName,
            title);
        notificationInfo.SetDataValue(
            NotificationProviderPrivateMessagingConsts.NotificationInfoContentPropertyName, content);
        notificationInfo.SetDataValue(
            NotificationProviderPrivateMessagingConsts.NotificationInfoSendFromCreatorPropertyName,
            sendFromCreator);
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

    public static bool GetPrivateMessagingSendFromCreator(this NotificationInfo notificationInfo)
    {
        return (bool)notificationInfo.GetDataValue(NotificationProviderPrivateMessagingConsts
            .NotificationInfoSendFromCreatorPropertyName);
    }
}