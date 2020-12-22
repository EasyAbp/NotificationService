using System.Collections.Generic;
using EasyAbp.NotificationService.NotificationInfos;
using JetBrains.Annotations;

namespace EasyAbp.NotificationService.Provider.Sms
{
    public static class NotificationInfoExtensions
    {
        public static void SetSmsData(this NotificationInfo notificationInfo, [NotNull] string text,
            [NotNull] IDictionary<string, object> properties)
        {
            notificationInfo.SetDataValue(NotificationProviderSmsConsts.NotificationInfoTextPropertyName, text);
            notificationInfo.SetDataValue(NotificationProviderSmsConsts.NotificationInfoPropertiesPropertyName, properties);
        }
    }
}