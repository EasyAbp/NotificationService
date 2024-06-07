using System.Collections.Generic;
using System.Threading.Tasks;
using EasyAbp.NotificationService.IntegrationServices;
using EasyAbp.NotificationService.Notifications;
using EasyAbp.NotificationService.Provider.Sms;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Volo.Abp.AspNetCore.Mvc;
using Volo.Abp.Json;
using Volo.Abp.Users;

namespace EasyAbp.NotificationService;

[Route("test")]
public class TestController : AbpControllerBase
{
    protected IJsonSerializer JsonSerializer => LazyServiceProvider.LazyGetRequiredService<IJsonSerializer>();

    protected INotificationIntegrationService NotificationIntegrationService =>
        LazyServiceProvider.LazyGetRequiredService<INotificationIntegrationService>();

    [Authorize]
    [HttpGet("send-test-notification")]
    public virtual async Task SendTestNotificationAsync()
    {
        var model = new CreateSmsNotificationEto(CurrentTenant.Id,
            new NotificationUserInfoModel(CurrentUser.GetId(), CurrentUser.UserName!), "test",
            new Dictionary<string, object>(), JsonSerializer);

        await NotificationIntegrationService.QuickSendAsync(model);
    }
}