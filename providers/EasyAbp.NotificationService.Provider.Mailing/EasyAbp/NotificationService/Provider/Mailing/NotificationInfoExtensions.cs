using EasyAbp.NotificationService.NotificationInfos;
using JetBrains.Annotations;

namespace EasyAbp.NotificationService.Provider.Mailing
{
    public static class NotificationInfoExtensions
    {
        public static void SetMailingData(this NotificationInfo notificationInfo, [NotNull] string subject,
            [CanBeNull] string body)
        {
            notificationInfo.SetDataValue(NotificationProviderMailingConsts.NotificationInfoSubjectPropertyName, subject);
            notificationInfo.SetDataValue(NotificationProviderMailingConsts.NotificationInfoBodyPropertyName, body);
        }
    }
}