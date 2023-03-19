using EasyAbp.NotificationService.Notifications;
using JetBrains.Annotations;
using Volo.Abp.Data;

namespace EasyAbp.NotificationService.Provider.Mailing;

public static class CreateNotificationInfoModelExtensions
{
    public static void SetSubject(this CreateNotificationInfoModel model, [NotNull] string subject)
    {
        model.SetProperty(nameof(CreateEmailNotificationEto.Subject), subject);
    }

    public static string GetSubject(this CreateNotificationInfoModel model)
    {
        return (string)model.GetProperty(nameof(CreateEmailNotificationEto.Subject));
    }

    public static void SetBody(this CreateNotificationInfoModel model, [CanBeNull] string body)
    {
        model.SetProperty(nameof(CreateEmailNotificationEto.Body), body);
    }

    public static string GetBody(this CreateNotificationInfoModel model)
    {
        return (string)model.GetProperty(nameof(CreateEmailNotificationEto.Body));
    }
}