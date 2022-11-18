using System;
using System.Collections.Generic;
using EasyAbp.NotificationService.Notifications;
using JetBrains.Annotations;

namespace EasyAbp.NotificationService.Provider.WeChatOfficial
{
    [Serializable]
    public class CreateWeChatOfficialTemplateMessageNotificationEto : CreateNotificationEto
    {
        [NotNull]
        public WeChatOfficialTemplateMessageDataModel DataModel { get; set; } = null!;

        protected CreateWeChatOfficialTemplateMessageNotificationEto()
        {
        }

        public CreateWeChatOfficialTemplateMessageNotificationEto(Guid? tenantId, IEnumerable<Guid> userIds,
            [NotNull] WeChatOfficialTemplateMessageDataModel dataModel) : base(tenantId, userIds)
        {
            DataModel = dataModel;
        }
    }
}