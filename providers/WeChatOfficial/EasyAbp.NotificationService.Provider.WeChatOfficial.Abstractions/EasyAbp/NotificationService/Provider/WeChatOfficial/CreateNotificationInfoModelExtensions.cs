using EasyAbp.NotificationService.Notifications;
using JetBrains.Annotations;
using Volo.Abp.Data;

namespace EasyAbp.NotificationService.Provider.WeChatOfficial;

public static class CreateNotificationInfoModelExtensions
{
    public static void SetDataModel(this CreateNotificationInfoModel model,
        [NotNull] WeChatOfficialTemplateMessageDataModel dataModel,
        ITemplateMessageDataModelJsonSerializer templateMessageDataModelJsonSerializer)
    {
        model.SetJsonDataModel(templateMessageDataModelJsonSerializer.Serialize(dataModel));
    }

    public static WeChatOfficialTemplateMessageDataModel GetDataModel(this CreateNotificationInfoModel model,
        ITemplateMessageDataModelJsonSerializer templateMessageDataModelJsonSerializer)
    {
        return templateMessageDataModelJsonSerializer.Deserialize(model.GetJsonDataModel());
    }

    public static void SetJsonDataModel(this CreateNotificationInfoModel model, [NotNull] string jsonDataModel)
    {
        model.SetProperty(nameof(CreateWeChatOfficialTemplateMessageNotificationEto.JsonDataModel), jsonDataModel);
    }

    public static string GetJsonDataModel(this CreateNotificationInfoModel model)
    {
        return (string)model.GetProperty(nameof(CreateWeChatOfficialTemplateMessageNotificationEto.JsonDataModel));
    }
}