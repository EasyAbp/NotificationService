using Newtonsoft.Json;
using Volo.Abp.DependencyInjection;

namespace EasyAbp.NotificationService.Provider.WeChatOfficial;

public class TemplateMessageDataModelJsonSerializer : ITemplateMessageDataModelJsonSerializer, ITransientDependency
{
    public virtual string Serialize(WeChatOfficialTemplateMessageDataModel dataModel)
    {
        return JsonConvert.SerializeObject(dataModel,
            new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore });
    }

    public virtual WeChatOfficialTemplateMessageDataModel Deserialize(string jsonString)
    {
        return JsonConvert.DeserializeObject<WeChatOfficialTemplateMessageDataModel>(jsonString);
    }
}