using System.Collections.Generic;
using System.Threading.Tasks;
using EasyAbp.NotificationService.Localization;
using EasyAbp.NotificationService.Permissions;
using Volo.Abp.UI.Navigation;

namespace EasyAbp.NotificationService.Web.Menus
{
    public class NotificationServiceMenuContributor : IMenuContributor
    {
        public async Task ConfigureMenuAsync(MenuConfigurationContext context)
        {
            if (context.Menu.Name == StandardMenus.Main)
            {
                await ConfigureMainMenu(context);
            }
        }

        private async Task ConfigureMainMenu(MenuConfigurationContext context)
        {
            var l = context.GetLocalizer<NotificationServiceResource>();
             //Add main menu items.

            var notificationServiceMenuItem = new ApplicationMenuItem(NotificationServiceMenus.Prefix,
                l["Menu:NotificationService"], icon: "fa fa-bell");
            
            if (await context.IsGrantedAsync(NotificationServicePermissions.Notification.Default))
            {
                notificationServiceMenuItem.AddItem(
                    new ApplicationMenuItem(NotificationServiceMenus.Notification, l["Menu:Notification"], "/NotificationService/Notifications/Notification")
                );
            }

            if (!notificationServiceMenuItem.Items.IsNullOrEmpty())
            {
                context.Menu.GetAdministration().AddItem(notificationServiceMenuItem);
            }
        }
    }
}
