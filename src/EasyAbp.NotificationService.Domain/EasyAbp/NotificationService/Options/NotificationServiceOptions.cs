namespace EasyAbp.NotificationService.Options;

public class NotificationServiceOptions
{
    public NotificationServiceProviderConfigurations Providers { get; set; } = new();
}