# NotificationService.Provider.WeChatOfficial

[![ABP version](https://img.shields.io/badge/dynamic/xml?style=flat-square&color=yellow&label=abp&query=%2F%2FProject%2FPropertyGroup%2FAbpVersion&url=https%3A%2F%2Fraw.githubusercontent.com%2FEasyAbp%2FNotificationService%2Fmaster%2FDirectory.Build.props)](https://abp.io)
[![NuGet](https://img.shields.io/nuget/v/EasyAbp.NotificationService.Domain.Shared.svg?style=flat-square)](https://www.nuget.org/packages/EasyAbp.NotificationService.Domain.Shared)
[![NuGet Download](https://img.shields.io/nuget/dt/EasyAbp.NotificationService.Domain.Shared.svg?style=flat-square)](https://www.nuget.org/packages/EasyAbp.NotificationService.Domain.Shared)
[![Discord online](https://badgen.net/discord/online-members/xyg8TrRa27?label=Discord)](https://discord.gg/xyg8TrRa27)
[![GitHub stars](https://img.shields.io/github/stars/EasyAbp/NotificationService?style=social)](https://www.github.com/EasyAbp/NotificationService)

微信服务号用户通知提供者。目前唯一的通知方式为公众号模板消息。

## Usage

You can create a notification using a notification factory or manually.

### 实现 `IWeChatOfficialUserOpenIdProvider`

你需要实现`IWeChatOfficialUserOpenIdProvider`，否则本模块无法获知目标用户的 OpenId。

Todo: demo

### Create with Notification Factory

1. Create a factory.
    ```csharp
    public class UserWelcomeNotificationFactory :
        NotificationFactory<UserWelcomeNotificationDataModel, CreateWeChatOfficialTemplateMessageNotificationEto>,
        ITransientDependency
    {
        // 如果 appId 为空，则自动获取配置的 AbpWeChatOfficialOptions
        // 如果你指定了 appId，请自行实现 IAbpWeChatOfficialOptionsProvider
        var appId = "my-official-appid";
        var templateData = new TemplateMessage($"Hello, {model.UserName}", "Thank you");

        templateData.AddKeywords("gift-card-code", model.GiftCardCode);

        var dataModel = new WeChatOfficialTemplateMessageDataModel("my-template-id", new MiniProgramRequest
        {
            AppId = "my-mini-program-appid",
            PagePath = "my-mini-program-page-path"
        }, "https://github.com", templateData, appId);

        return Task.FromResult(
            new CreateWeChatOfficialTemplateMessageNotificationEto(CurrentTenant.Id, userIds, dataModel));
    }
    ```

2. Use the factory to create a notification and publish it.
    ```csharp
    var eto = await userWelcomeNotificationFactory.CreateAsync(
        model: new UserWelcomeNotificationDataModel(userData.UserName, giftCardCode),
        userId: userData.Id
    );
    
    await distributedEventBus.PublishAsync(eto);
    ```

### Create Manually

Publish the notification.

```csharp
await distributedEventBus.PublishAsync(
    new CreateWeChatOfficialTemplateMessageNotificationEto(
        CurrentTenant.Id, userIds, dataModel));
```
