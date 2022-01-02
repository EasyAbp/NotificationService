# NotificationService

[![ABP version](https://img.shields.io/badge/dynamic/xml?style=flat-square&color=yellow&label=abp&query=%2F%2FProject%2FPropertyGroup%2FAbpVersion&url=https%3A%2F%2Fraw.githubusercontent.com%2FEasyAbp%2FNotificationService%2Fmaster%2FDirectory.Build.props)](https://abp.io)
[![NuGet](https://img.shields.io/nuget/v/EasyAbp.NotificationService.Domain.Shared.svg?style=flat-square)](https://www.nuget.org/packages/EasyAbp.NotificationService.Domain.Shared)
[![NuGet Download](https://img.shields.io/nuget/dt/EasyAbp.NotificationService.Domain.Shared.svg?style=flat-square)](https://www.nuget.org/packages/EasyAbp.NotificationService.Domain.Shared)
[![Discord online](https://badgen.net/discord/online-members/S6QaezrCRq?label=Discord)](https://discord.gg/S6QaezrCRq)
[![GitHub stars](https://img.shields.io/github/stars/EasyAbp/NotificationService?style=social)](https://www.github.com/EasyAbp/NotificationService)

An integrated user notification service Abp module, supporting email, SMS, PM, and more other methods.

## Online Demo

We have launched an online demo for this module: [https://notification.samples.easyabp.io](https://notification.samples.easyabp.io)

## Installation

1. Install the following NuGet packages. ([see how](https://github.com/EasyAbp/EasyAbpGuide/blob/master/docs/How-To.md#add-nuget-packages))

    * EasyAbp.NotificationService.Application
    * EasyAbp.NotificationService.Application.Contracts
    * EasyAbp.NotificationService.Domain
    * EasyAbp.NotificationService.Domain.Shared
    * EasyAbp.NotificationService.EntityFrameworkCore
    * EasyAbp.NotificationService.HttpApi
    * EasyAbp.NotificationService.HttpApi.Client
    * (Optional) EasyAbp.NotificationService.MongoDB
    * (Optional) EasyAbp.NotificationService.Web
    * (Optional) EasyAbp.NotificationService.Provider.Mailing
    * (Optional) EasyAbp.NotificationService.Provider.PrivateMessaging
    * (Optional) EasyAbp.NotificationService.Provider.Sms

1. Add `DependsOn(typeof(NotificationServiceXxxModule))` attribute to configure the module dependencies. ([see how](https://github.com/EasyAbp/EasyAbpGuide/blob/master/docs/How-To.md#add-module-dependencies))

1. Add `builder.ConfigureNotificationService();` to the `OnModelCreating()` method in **MyProjectMigrationsDbContext.cs**.

1. Add EF Core migrations and update your database. See: [ABP document](https://docs.abp.io/en/abp/latest/Tutorials/Part-1?UI=MVC&DB=EF#add-database-migration).

## Usage

You can create a notification using a notification factory or manually.

### Create with Notification Factory

1. Create a factory.
    ```csharp
    public class UserWelcomeNotificationFactory
        : NotificationFactory<UserWelcomeNotificationDataModel, CreateSmsNotificationEto>, ITransientDependency
    {
        public override async Task<CreateSmsNotificationEto> CreateAsync(
            UserWelcomeNotificationDataModel model, IEnumerable<Guid> userIds)
        {
            var text = $"Hello, {model.UserName}, here is a gift card code for you: {model.GiftCardCode}";

            return new CreateSmsNotificationEto(userIds, text, new Dictionary<string, object>());
        }
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
await distributedEventBus.PublishAsync(new CreateEmailNotificationEto(userIds, subject, body));
```

![Notifications](/docs/images/Notifications.png)

## Q&A

### How to Change User's Email Address and Phone Number Info Source

You can override the [IdentityUserEmailAddressProvider](https://github.com/EasyAbp/NotificationService/blob/master/providers/Mailing/EasyAbp.NotificationService.Provider.Mailing/EasyAbp/NotificationService/Provider/Mailing/IdentityUserEmailAddressProvider.cs) and the [IdentityUserPhoneNumberProvider](https://github.com/EasyAbp/NotificationService/blob/master/providers/Sms/EasyAbp.NotificationService.Provider.Sms/EasyAbp/NotificationService/Provider/Sms/IdentityUserPhoneNumberProvider.cs).

### How to Use a Dynamic Notification Content Template

You can use the [ABP Text Templating](https://docs.abp.io/en/abp/latest/Text-Templating) feature, see the [demo](https://github.com/EasyAbp/NotificationService/blob/master/providers/Mailing/EasyAbp.NotificationService.Provider.Mailing.Test/EasyAbp/NotificationService/Provider/Mailing/UserWelcomeNotifications/UserWelcomeNotificationFactory.cs).

## Road map

- [ ] Private messaging notification provider.
- [ ] WeChat uniform message notification provider.
- [ ] Notification management UI.
