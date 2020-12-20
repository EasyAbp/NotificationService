using Volo.Abp;

namespace EasyAbp.NotificationService.Notifications
{
    public class NotificationHasBeenCompletedException : BusinessException
    {
        public NotificationHasBeenCompletedException() : base(
            code: "NotificationHasBeenCompleted",
            message: "The notification has been completed.")
        {
        }
    }
}