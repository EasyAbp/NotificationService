namespace EasyAbp.NotificationService.Provider.WeChatOfficial;

public interface ITemplateMessageDataModelJsonSerializer
{
    string Serialize(WeChatOfficialTemplateMessageDataModel dataModel);

    WeChatOfficialTemplateMessageDataModel Deserialize(string jsonString);
}