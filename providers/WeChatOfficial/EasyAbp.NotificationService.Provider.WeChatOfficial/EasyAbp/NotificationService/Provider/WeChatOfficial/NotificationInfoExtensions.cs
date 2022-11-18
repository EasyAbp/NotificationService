using EasyAbp.Abp.WeChat.Official.Services.TemplateMessage.Request;
using EasyAbp.NotificationService.NotificationInfos;
using JetBrains.Annotations;
using Volo.Abp;
using Volo.Abp.Json;

namespace EasyAbp.NotificationService.Provider.WeChatOfficial
{
    public static class NotificationInfoExtensions
    {
        public static void SetWeChatOfficialTemplateMessageData(this NotificationInfo notificationInfo,
            [NotNull] WeChatOfficialTemplateMessageDataModel dataModel,
            ITemplateMessageDataModelJsonSerializer jsonSerializer)
        {
            notificationInfo.SetDataValue(
                NotificationProviderWeChatOfficialConsts.NotificationInfoDataModelPropertyName,
                jsonSerializer.Serialize(Check.NotNull(dataModel, nameof(dataModel))));
        }

        public static WeChatOfficialTemplateMessageDataModel GetWeChatOfficialTemplateMessageData(
            this NotificationInfo notificationInfo, ITemplateMessageDataModelJsonSerializer jsonSerializer)
        {
            return jsonSerializer.Deserialize(
                notificationInfo.GetDataValue(NotificationProviderWeChatOfficialConsts
                    .NotificationInfoDataModelPropertyName) as string);
        }
    }
}