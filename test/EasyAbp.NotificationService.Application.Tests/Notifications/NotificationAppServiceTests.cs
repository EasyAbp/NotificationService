using Shouldly;
using System.Threading.Tasks;
using Xunit;

namespace EasyAbp.NotificationService.Notifications
{
    public class NotificationAppServiceTests : NotificationServiceApplicationTestBase
    {
        private readonly INotificationAppService _notificationAppService;

        public NotificationAppServiceTests()
        {
            _notificationAppService = GetRequiredService<INotificationAppService>();
        }

        /*
        [Fact]
        public async Task Test1()
        {
            // Arrange

            // Act

            // Assert
        }
        */
    }
}
