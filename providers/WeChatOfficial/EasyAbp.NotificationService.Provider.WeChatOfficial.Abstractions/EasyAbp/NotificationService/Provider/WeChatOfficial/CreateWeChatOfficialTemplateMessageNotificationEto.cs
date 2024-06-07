using System;
using System.Collections.Generic;
using EasyAbp.NotificationService.Notifications;
using JetBrains.Annotations;
using Volo.Abp.Data;
using Volo.Abp.Json;
using Volo.Abp.MultiTenancy;

namespace EasyAbp.NotificationService.Provider.WeChatOfficial;

[Serializable]
public class CreateWeChatOfficialTemplateMessageNotificationEto : CreateNotificationInfoModel, IMultiTenant
{
    public Guid? TenantId { get; set; }

    [NotNull]
    public string JsonDataModel
    {
        get => this.GetJsonDataModel();
        set => this.SetJsonDataModel(value);
    }

    public CreateWeChatOfficialTemplateMessageNotificationEto()
    {
    }

    public CreateWeChatOfficialTemplateMessageNotificationEto(Guid? tenantId,
        IEnumerable<NotificationUserInfoModel> users,
        [NotNull] WeChatOfficialTemplateMessageDataModel dataModel,
        ITemplateMessageDataModelJsonSerializer templateMessageDataModelJsonSerializer) : base(
        NotificationProviderWeChatOfficialConsts.TemplateMessageNotificationMethod, users)
    {
        TenantId = tenantId;
        this.SetDataModel(dataModel, templateMessageDataModelJsonSerializer);
    }

    public CreateWeChatOfficialTemplateMessageNotificationEto(Guid? tenantId, IEnumerable<Guid> userIds,
        [NotNull] WeChatOfficialTemplateMessageDataModel dataModel,
        ITemplateMessageDataModelJsonSerializer templateMessageDataModelJsonSerializer) : base(
        NotificationProviderWeChatOfficialConsts.TemplateMessageNotificationMethod, userIds)
    {
        TenantId = tenantId;
        this.SetDataModel(dataModel, templateMessageDataModelJsonSerializer);
    }

    public CreateWeChatOfficialTemplateMessageNotificationEto(Guid? tenantId, Guid userId,
        [NotNull] WeChatOfficialTemplateMessageDataModel dataModel,
        ITemplateMessageDataModelJsonSerializer templateMessageDataModelJsonSerializer) : base(
        NotificationProviderWeChatOfficialConsts.TemplateMessageNotificationMethod, userId)
    {
        TenantId = tenantId;
        this.SetDataModel(dataModel, templateMessageDataModelJsonSerializer);
    }

    public CreateWeChatOfficialTemplateMessageNotificationEto(Guid? tenantId, NotificationUserInfoModel user,
        [NotNull] WeChatOfficialTemplateMessageDataModel dataModel,
        ITemplateMessageDataModelJsonSerializer templateMessageDataModelJsonSerializer) : base(
        NotificationProviderWeChatOfficialConsts.TemplateMessageNotificationMethod, user)
    {
        TenantId = tenantId;
        this.SetDataModel(dataModel, templateMessageDataModelJsonSerializer);
    }
}