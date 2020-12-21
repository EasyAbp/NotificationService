using Volo.Abp.TextTemplating;

namespace EasyAbp.NotificationService.Provider.Mailing.UserWelcomeNotifications
{
    public class UserWelcomeEmailTemplateDefinitionProvider : TemplateDefinitionProvider
    {
        public override void Define(ITemplateDefinitionContext context)
        {
            context.Add(
                new TemplateDefinition("UserWelcomeEmailSubject")
                    .WithVirtualFilePath(
                        "/EasyAbp/NotificationService/Provider/Mailing/UserWelcomeNotifications/UserWelcomeEmailSubject.tpl",
                        isInlineLocalized: true
                    )
            );
            
            context.Add(
                new TemplateDefinition("UserWelcomeEmailBody")
                    .WithVirtualFilePath(
                        "/EasyAbp/NotificationService/Provider/Mailing/UserWelcomeNotifications/UserWelcomeEmailBody.tpl",
                        isInlineLocalized: true
                    )
            );
        }
    }
}