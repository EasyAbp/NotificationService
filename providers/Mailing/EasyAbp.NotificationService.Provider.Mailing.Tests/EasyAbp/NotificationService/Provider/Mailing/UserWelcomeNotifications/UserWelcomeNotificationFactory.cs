using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using EasyAbp.NotificationService.Notifications;
using Volo.Abp.DependencyInjection;
using Volo.Abp.TextTemplating;

namespace EasyAbp.NotificationService.Provider.Mailing.UserWelcomeNotifications
{
    public class UserWelcomeNotificationFactory : NotificationFactory<UserWelcomeNotificationDataModel, CreateEmailNotificationEto>, ITransientDependency
    {
        private readonly ITemplateRenderer _templateRenderer;

        public UserWelcomeNotificationFactory(ITemplateRenderer templateRenderer)
        {
            _templateRenderer = templateRenderer;
        }

        public override async Task<CreateEmailNotificationEto> CreateAsync(UserWelcomeNotificationDataModel model, IEnumerable<Guid> userIds)
        {
            var subject = await _templateRenderer.RenderAsync("UserWelcomeEmailSubject", model);
            
            var body = await _templateRenderer.RenderAsync("UserWelcomeEmailBody", model);
            
            return new CreateEmailNotificationEto(CurrentTenant.Id, userIds, subject, body);
        }
    }
}