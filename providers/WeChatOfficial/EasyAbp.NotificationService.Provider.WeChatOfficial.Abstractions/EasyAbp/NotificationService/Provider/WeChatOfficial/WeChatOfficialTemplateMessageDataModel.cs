using System;
using System.Text.Json.Serialization;
using EasyAbp.Abp.WeChat.Official.Services.TemplateMessage;
using EasyAbp.Abp.WeChat.Official.Services.TemplateMessage.Request;
using JetBrains.Annotations;

namespace EasyAbp.NotificationService.Provider.WeChatOfficial;

[Serializable]
public class WeChatOfficialTemplateMessageDataModel
{
    [NotNull]
    public string TemplateId { get; set; } = null!;

    [CanBeNull]
    public MiniProgramRequest MiniProgram { get; set; }

    [CanBeNull]
    public string Url { get; set; }

    [NotNull]
    [JsonInclude]
    public TemplateMessage Data { get; set; } = null!;

    /// <summary>
    /// Use the configured default AppId of the Abp.WeChat module if you don't set this value.
    /// </summary>
    [CanBeNull]
    public string AppId { get; set; }

    protected WeChatOfficialTemplateMessageDataModel()
    {
    }

    public WeChatOfficialTemplateMessageDataModel([NotNull] string templateId,
        [CanBeNull] MiniProgramRequest miniProgram, [CanBeNull] string url, [NotNull] TemplateMessage data,
        [CanBeNull] string appId)
    {
        TemplateId = templateId;
        MiniProgram = miniProgram;
        Url = url;
        Data = data;
        AppId = appId;
    }
}