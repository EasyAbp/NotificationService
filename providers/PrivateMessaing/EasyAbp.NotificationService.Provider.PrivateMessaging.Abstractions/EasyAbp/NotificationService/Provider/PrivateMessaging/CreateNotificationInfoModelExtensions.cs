using EasyAbp.NotificationService.Notifications;
using JetBrains.Annotations;
using Volo.Abp.Data;

namespace EasyAbp.NotificationService.Provider.PrivateMessaging;

public static class CreateNotificationInfoModelExtensions
{
    public static void SetTitle(this CreateNotificationInfoModel model, [NotNull] string title)
    {
        model.SetProperty(nameof(CreatePrivateMessageNotificationEto.Title), title);
    }

    public static string GetTitle(this CreateNotificationInfoModel model)
    {
        return (string)model.GetProperty(nameof(CreatePrivateMessageNotificationEto.Title));
    }

    public static void SetContent(this CreateNotificationInfoModel model, [CanBeNull] string content)
    {
        model.SetProperty(nameof(CreatePrivateMessageNotificationEto.Content), content);
    }

    public static string GetContent(this CreateNotificationInfoModel model)
    {
        return (string)model.GetProperty(nameof(CreatePrivateMessageNotificationEto.Content));
    }

    public static void SetSendFromCreator(this CreateNotificationInfoModel model, bool sendFromCreator)
    {
        model.SetProperty(nameof(CreatePrivateMessageNotificationEto.SendFromCreator), sendFromCreator);
    }

    public static bool GetSendFromCreator(this CreateNotificationInfoModel model)
    {
        return (bool)model.GetProperty(nameof(CreatePrivateMessageNotificationEto.SendFromCreator));
    }
}