using System.Collections.Generic;
using EasyAbp.NotificationService.NotificationInfos;
using JetBrains.Annotations;

namespace EasyAbp.NotificationService.Provider.Sms;

public static class NotificationInfoExtensions
{
    public static void SetSmsData(this NotificationInfo notificationInfo, [NotNull] string text,
        [NotNull] string jsonProperties)
    {
        notificationInfo.SetDataValue(NotificationProviderSmsConsts.NotificationInfoTextPropertyName, text);
        notificationInfo.SetDataValue(NotificationProviderSmsConsts.NotificationInfoJsonPropertiesPropertyName,
            jsonProperties);
    }

    public static string GetSmsText(this NotificationInfo notificationInfo)
    {
        return (string)notificationInfo.GetDataValue(NotificationProviderSmsConsts.NotificationInfoTextPropertyName);
    }

    public static string GetSmsJsonProperties(this NotificationInfo notificationInfo)
    {
        return (string)notificationInfo.GetDataValue(NotificationProviderSmsConsts
            .NotificationInfoJsonPropertiesPropertyName);
    }
}