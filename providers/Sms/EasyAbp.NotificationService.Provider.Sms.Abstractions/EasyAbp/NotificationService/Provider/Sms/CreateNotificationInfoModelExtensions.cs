using System.Collections.Generic;
using EasyAbp.NotificationService.Notifications;
using JetBrains.Annotations;
using Volo.Abp.Data;
using Volo.Abp.Json;

namespace EasyAbp.NotificationService.Provider.Sms;

public static class CreateNotificationInfoModelExtensions
{
    public static void SetText(this CreateNotificationInfoModel model, [NotNull] string text)
    {
        model.SetProperty(nameof(CreateSmsNotificationEto.Text), text);
    }

    public static string GetText(this CreateNotificationInfoModel model)
    {
        return (string)model.GetProperty(nameof(CreateSmsNotificationEto.Text));
    }

    public static void SetProperties(this CreateNotificationInfoModel model,
        [NotNull] IDictionary<string, object> properties, IJsonSerializer jsonSerializer)
    {
        model.SetJsonProperties(jsonSerializer.Serialize(properties));
    }

    public static IDictionary<string, object> GetProperties(this CreateNotificationInfoModel model,
        IJsonSerializer jsonSerializer)
    {
        return jsonSerializer.Deserialize<IDictionary<string, object>>(model.GetJsonProperties());
    }

    public static void SetJsonProperties(this CreateNotificationInfoModel model, [NotNull] string jsonProperties)
    {
        model.SetProperty(nameof(CreateSmsNotificationEto.JsonProperties), jsonProperties);
    }

    public static string GetJsonProperties(this CreateNotificationInfoModel model)
    {
        return (string)model.GetProperty(nameof(CreateSmsNotificationEto.JsonProperties));
    }
}